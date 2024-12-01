using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;

using LilaRent.MobileApp.Converters;
using LilaRent.MobileApp.Core;


namespace LilaRent.MobileApp.MarkupExtensions;


public partial class LocalizeFormatExtension
{
    private abstract class Strategy : BaseStrategy
    {
        public Strategy(ILocalizationManager localizationManager)
            : base(localizationManager) 
        { }

        public abstract BindingBase ProvideValue(object? key = null, object? format = null, IValueConverter? converter = null);
    }


    private class StaticKeyStaticFormatStrategy : Strategy
    {
        public StaticKeyStaticFormatStrategy(ILocalizationManager localizationManager)
           : base(localizationManager)
        { }


        public override BindingBase ProvideValue(object? key = null, object? format = null, IValueConverter? converter = null)
        {
            ArgumentNullException 
                .ThrowIfNull(key, nameof(key));

            ArgumentException
                .ThrowIfNullOrWhiteSpace(format?.ToString(), nameof(format));

            return new Binding
            {
                Mode = BindingMode.OneWay,
                Path = $"[{format}]",
                Source = _localizationManager,

                Converter = new FuncConverter<string, string>
                (
                    convert: (string? localizedFormat) =>
                    {
                        if (key is null)
                            throw new InvalidOperationException("Key is null.");

                        ArgumentNullException
                            .ThrowIfNull(localizedFormat, nameof(localizedFormat));

                        if (converter is not null)
                        {
                            key = converter.Convert(key, typeof(string), null!, null!)
                                ?? throw new InvalidOperationException("Object turns null after converter convert.");
                        }

                        return new StringFormatMultiConverter()
                            .Convert([localizedFormat, key], typeof(string), null!, null!)
                            .ToString()!;
                    },

                    convertBack: null
                )
            };
        }
    }


    private class BindedKeyStaticFormatStrategy : Strategy
    {
        public BindedKeyStaticFormatStrategy(ILocalizationManager localizationManager)
            : base(localizationManager)
        { }

        public override BindingBase ProvideValue(object? key = null, object? format = null, IValueConverter? converter = null)
        {
            ArgumentNullException
               .ThrowIfNull(key, nameof(key));

            ArgumentException
                .ThrowIfNullOrWhiteSpace(format?.ToString(), nameof(format));


            if (key is not BindingBase bindedKey)
                throw new ArgumentException("Key must be binding base instance.", nameof(key));


            return new MultiBinding
            {
                Bindings = new Collection<BindingBase>
                {
                    bindedKey,

                    new Binding
                    {
                        Mode = BindingMode.OneWay,
                        Path = $"[{format}]",
                        Source = _localizationManager
                    },
                },

                Converter = new FuncMultiConverter<object, string, string>
                (
                    convert: (object key, string localizedFormat) =>
                    {
                        if (key is null && localizedFormat is null)
                            return string.Empty;

                        if (key is null)
                            return localizedFormat!;

                        if (converter is not null)
                        {
                            key = converter.Convert(key, key.GetType(), null, null!)
                                ?? throw new InvalidOperationException("Object turns null after converter convert.");
                        }

                        return new StringFormatMultiConverter()
                            .Convert([localizedFormat!, key], typeof(string), null!, null!)
                            .ToString()!;
                    },

                    convertBack: null
                )
            };
        }
    }

    private class StaticKeyBindedFormatStrategy : Strategy
    {
        public StaticKeyBindedFormatStrategy(ILocalizationManager localizationManager)
            : base(localizationManager)
        { }

        public override BindingBase ProvideValue(object? key = null, object? format = null, IValueConverter? converter = null)
        {
            ArgumentNullException
                .ThrowIfNull(key, nameof(key));

            ArgumentNullException
                .ThrowIfNull(format, nameof(format));


            if (format is not BindingBase bindedFormat)
                throw new ArgumentException("Format must be binding base instance.", nameof(format));


            return new MultiBinding
            {
                Bindings = new Collection<BindingBase>
                {
                    CreateFictitiousTriggeredBinding(_localizationManager),

                    bindedFormat,
                },

                Converter = new FuncMultiConverter<byte, string, string>
                (
                    convert: (byte fictitious, string format) =>
                    {
                        if (key is null && format is null)
                            return string.Empty;


                        if (format is not null)
                            format = _localizationManager[format].ToString()!;

                        if (key is null)
                            return format!;


                        if (converter is not null)
                        {
                            key = converter.Convert(key, typeof(string), null, null!)
                                ?? throw new InvalidOperationException("Object turns null after converter convert.");
                        }

                        if (format is null)
                        {
                            return key.ToString()!;
                        }
                        else
                        {
                            return new StringFormatMultiConverter()
                                .Convert([format, key], typeof(string), null!, null!)
                                .ToString()!;
                        }
                    },

                    convertBack: null
                )
            };
        }
    }

    private class BindedKeyBindedFormatStrategy : Strategy
    {
        public BindedKeyBindedFormatStrategy(ILocalizationManager localizationManager) 
            : base(localizationManager) 
        { }


        public override BindingBase ProvideValue(object? key = null, object? format = null, IValueConverter? converter = null)
        {
            ArgumentNullException
                .ThrowIfNull(key, nameof(key));

            ArgumentNullException
                .ThrowIfNull(format, nameof(format));

            if (key is not BindingBase bindedKey)
                throw new ArgumentException("Key must be binding base instance.", nameof(key));

            if (format is not BindingBase bindedFormat)
                throw new ArgumentException("Format must be binding base instance.", nameof(key));


            return new MultiBinding
            {
                Bindings = new Collection<BindingBase>
                {
                    CreateFictitiousTriggeredBinding(_localizationManager),

                    bindedKey,
                    bindedFormat,   
                },

                Converter = new FuncMultiConverter<byte, object, string, string>
                (
                    convert: (byte fictitious, object? key, string? format) =>
                    {
                        if (key is null && format is null)
                            return string.Empty;


                        if (format is not null)                        
                            format = _localizationManager[format].ToString();

                        if (key is null)
                            return format!;


                        if (converter is not null)
                        {
                            key = converter.Convert(key, typeof(object), null, null!)!
                                ?? throw new InvalidOperationException("Object turns null after converter convert.");
                        }              

                        if (format is null)
                        {
                            return key.ToString()!;
                        }
                        else
                        {
                            return new StringFormatMultiConverter()
                                .Convert([format, key], typeof(string), null!, null!)
                                .ToString()!;          
                        }
                    },

                    convertBack: null
                ),
            };
        }
    }
}
