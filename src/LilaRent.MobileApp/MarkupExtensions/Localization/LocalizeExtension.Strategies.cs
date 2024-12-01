using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;

using LilaRent.MobileApp.Converters;
using LilaRent.MobileApp.Core;


namespace LilaRent.MobileApp.MarkupExtensions;


public partial class LocalizeExtension
{
    private abstract class Strategy : BaseStrategy
    {
        public Strategy(ILocalizationManager localizationManager) 
            : base(localizationManager) 
        { }

        public abstract BindingBase ProvideValue(
            object? key = null, 
            object? format = null, 
            IValueConverter? initialConverter = null,
            IValueConverter? finalConverter = null);
    }


    private class StaticKeyStrategy : Strategy
    {
        public StaticKeyStrategy(ILocalizationManager localizationManager) 
            : base(localizationManager) 
        { }


        public override BindingBase ProvideValue(
            object? key = null, 
            object? format = null, 
            IValueConverter? initialConverter = null,
            IValueConverter? finalConverter = null)
        {
            ArgumentNullException
                .ThrowIfNull(key, nameof(key));

            return CreateFictitiousTriggeredBinding(_localizationManager, new FuncConverter<byte, string, object?>
            (
                convert: (byte fictitious, object? param, CultureInfo info) =>
                {
                    if (key is null)
                        throw new InvalidOperationException("Key is null.");

                    if (initialConverter is not null)
                    {
                        key = initialConverter.Convert(key, typeof(object), param, info)
                            ?? throw new InvalidOperationException("Object turns null after initialConverter convert.");
                    }

                    var localized = _localizationManager[key.ToString()!]
                        ?? throw new InvalidOperationException("Object turns null after localization.");

                    if (finalConverter is not null)
                    {
                        localized = finalConverter.Convert(localized, localized.GetType(), param, info)
                            ?? throw new InvalidOperationException("Object turns null after finalConverter convert.");
                    }

                    return localized.ToString()!;
                },

                convertBack: null
            ));
        }
    }

    private class StaticKeyStaticFormatStrategy : Strategy
    {
        public StaticKeyStaticFormatStrategy(ILocalizationManager localizationManager)
           : base(localizationManager)
        { }


        public override BindingBase ProvideValue(
            object? key = null, 
            object? format = null, 
            IValueConverter? initialConverter = null, 
            IValueConverter? finalConverter = null)
        {
            ArgumentNullException 
                .ThrowIfNull(key, nameof(key));

            ArgumentException
                .ThrowIfNullOrWhiteSpace(format?.ToString(), nameof(format));

            return new MultiBinding
            {
                Bindings = new Collection<BindingBase>
                {
                    CreateFictitiousTriggeredBinding(_localizationManager),

                    new Binding
                    {
                        Mode = BindingMode.OneWay,
                        Path = $"[{format}]",
                        Source = _localizationManager
                    },
                },

                Converter = new FuncMultiConverter<byte, string, string>
                (
                    convert: (byte fictitious, string localizedFormat) =>
                    {
                        if (key is null)
                            throw new InvalidOperationException("Key is null.");

                        ArgumentNullException
                            .ThrowIfNull(localizedFormat, nameof(localizedFormat));

                        if (initialConverter is not null)
                        {
                            key = initialConverter.Convert(key, typeof(string), null, null!)
                                ?? throw new InvalidOperationException("Object turns null after initialConverter convert.");
                        }

                        var localized = _localizationManager[key.ToString()!]
                            ?? throw new InvalidOperationException("Object turns null after localization.");

                        if (finalConverter is not null)
                        {
                            localized = finalConverter.Convert(localized, localized.GetType(), null, null!)
                                ?? throw new InvalidOperationException("Object turns null after finalConverter convert.");
                        }

                        return new StringFormatMultiConverter()
                            .Convert([localizedFormat, localized], typeof(string), null!, null!)
                            .ToString()!;
                    },

                    convertBack: null
                )
            };
        }
    }


    private class BindedKeyStrategy : Strategy
    {
        public BindedKeyStrategy(ILocalizationManager localizationManager)
           : base(localizationManager)
        { }


        public override BindingBase ProvideValue(
            object? key = null, 
            object? format = null, 
            IValueConverter? initialConverter = null, 
            IValueConverter? finalConverter = null)
        {
            ArgumentNullException
                .ThrowIfNull(key, nameof(key));

            if (key is not BindingBase bindedKey)
                throw new ArgumentException("Key must be binding base instance.", nameof(key));

            return new MultiBinding
            {
                Bindings = new Collection<BindingBase>
                {
                    CreateFictitiousTriggeredBinding(_localizationManager),

                    bindedKey,
                },

                Converter = new FuncMultiConverter<byte, object, string>
                (
                    convert: (byte fictitious, object key) => 
                    {
                        if (key is null)
                            return string.Empty;

                        if (initialConverter is not null)
                        {
                            key = initialConverter.Convert(key, key.GetType(), null, null!)
                                ?? throw new InvalidOperationException("Object turns null after initialConverter convert.");
                        }

                        var localized = _localizationManager[key.ToString()!]
                            ?? throw new InvalidOperationException("Object turns null after localization.");

                        if (finalConverter is not null)
                        {
                            localized = finalConverter.Convert(localized, localized.GetType(), null, null!)
                                ?? throw new InvalidOperationException("Object turns null after finalConverter convert.");
                        }

                        return localized.ToString()!;
                    },

                    convertBack: null
                ),
            };
        }
    }

    private class BindedKeyStaticFormatStrategy : Strategy
    {
        public BindedKeyStaticFormatStrategy(ILocalizationManager localizationManager)
            : base(localizationManager)
        { }

        public override BindingBase ProvideValue(
            object? key = null,
            object? format = null,
            IValueConverter? initialConverter = null,
            IValueConverter? finalConverter = null)
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
                    CreateFictitiousTriggeredBinding(_localizationManager),

                    bindedKey,

                    new Binding
                    {
                        Mode = BindingMode.OneWay,
                        Path = $"[{format}]",
                        Source = _localizationManager
                    },
                },

                Converter = new FuncMultiConverter<byte, object, string, string>
                (
                    convert: (byte fictitious, object? key, string? localizedFormat) =>
                    {
                        if (key is null && localizedFormat is null)
                            return string.Empty;

                        if (key is null)
                            return localizedFormat!;


                        if (initialConverter is not null)
                        {
                            key = initialConverter.Convert(key, key.GetType(), null, null!)
                                ?? throw new InvalidOperationException("Object turns null after initialConverter convert.");
                        }

                        var localized = _localizationManager[key.ToString()!]
                            ?? throw new InvalidOperationException("Object turns null after localization.");

                        if (finalConverter is not null)
                        {
                            localized = finalConverter.Convert(localized, localized.GetType(), null, null!)
                                ?? throw new InvalidOperationException("Object turns null after finalConverter convert.");
                        }

                        return new StringFormatMultiConverter()
                            .Convert([localizedFormat!, localized], typeof(string), null!, null!)
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

        public override BindingBase ProvideValue(
            object? key = null,
            object? format = null,
            IValueConverter? initialConverter = null,
            IValueConverter? finalConverter = null)
        {
            ArgumentNullException
                .ThrowIfNull(key, nameof(key));

            ArgumentNullException
                .ThrowIfNull(format, nameof(format));


            if (format is not BindingBase bindedFormat)
                throw new ArgumentException("Format must be binding base instance.", nameof(key));


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


                        if (initialConverter is not null)
                        {
                            key = initialConverter.Convert(key, key.GetType(), null, null!)
                                ?? throw new InvalidOperationException("Object turns null after initialConverter convert.");
                        }

                        var localized = _localizationManager[key.ToString()!]
                            ?? throw new InvalidOperationException("Object turns null after localization.");

                        if (finalConverter is not null)
                        {
                            localized = finalConverter.Convert(localized, localized.GetType(), null, null!)
                                ?? throw new InvalidOperationException("Object turns null after finalConverter convert.");
                        }


                        if (format is null)
                        {
                            return localized.ToString()!;
                        }
                        else
                        {
                            return new StringFormatMultiConverter()
                                .Convert([format, localized], typeof(string), null!, null!)
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


        public override BindingBase ProvideValue(
            object? key = null, 
            object? format = null, 
            IValueConverter? initialConverter = null, 
            IValueConverter? finalConverter = null)
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


                        if (initialConverter is not null)
                        {
                            key = initialConverter.Convert(key, typeof(object), null, null!)!
                                ?? throw new InvalidOperationException("Object turns null after initialConverter convert.");
                        }

                        var localized = _localizationManager[key!.ToString()!]
                            ?? throw new InvalidOperationException("Object turns null after localization.");

                        if (finalConverter is not null)
                        {
                            localized = finalConverter.Convert(localized, localized.GetType(), null, null!)
                                ?? throw new InvalidOperationException("Object turns null after finalConverter convert.");
                        }

                        if (format is null)
                        {
                            return localized.ToString()!;
                        }
                        else
                        {
                            return new StringFormatMultiConverter()
                                .Convert([format, localized], typeof(string), null!, null!)
                                .ToString()!;          
                        }
                    },

                    convertBack: null
                ),
            };
        }
    }
}
