using LilaRent.MobileApp.Converters;
using LilaRent.MobileApp.Core;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LilaRent.MobileApp.MarkupExtensions;


public partial class MultiLocalizeFormatExtension
{
    private abstract class Strategy : BaseStrategy
    {
        public Strategy(ILocalizationManager localizationManager)
            : base(localizationManager)
        { }

        public abstract BindingBase ProvideValue(IEnumerable<object> keys, object format);
    }


    private class StaticFormatStrategy : Strategy
    {
        public StaticFormatStrategy(ILocalizationManager localizationManager)
            : base(localizationManager) 
        { }


        public override BindingBase ProvideValue(IEnumerable<object> keys, object format)
        {
            ArgumentNullException
               .ThrowIfNull(keys, nameof(keys));

            ArgumentNullException
                .ThrowIfNull(format, nameof(format));


            var multiBinding = new MultiBinding
            {
                Converter = new FuncMultiConverter<string, string>
                (
                    convert: (object[] values) =>
                    {
                        ArgumentNullException 
                            .ThrowIfNull(values, nameof(values));

                        if (values.Length == 0)
                            throw new ArgumentException("Values array must be not empty.");

                        return new StringFormatMultiConverter()
                            .Convert(values, typeof(string), parameter: null!, CultureInfo.CurrentCulture)
                            .ToString()!;
                    },

                    convertBack: null
                )
            };

            multiBinding.Bindings.Add(new Binding
            {
                Mode = BindingMode.OneWay,
                Path = $"[{format}]",
                Source = _localizationManager
            });

            foreach (var key in keys)
            {
                if (key is not BindingBase binding)
                {
                    binding = new Binding
                    {
                        Mode = BindingMode.OneTime,
                        Source = key,
                    };
                }

                multiBinding.Bindings.Add(binding);
            }

            return multiBinding;
        }
    }


    private class BindedFormatStrategy : Strategy
    {
        public BindedFormatStrategy(ILocalizationManager localizationManager)
            : base(localizationManager)
        { }

        public override BindingBase ProvideValue(IEnumerable<object> keys, object format)
        {
            ArgumentNullException
               .ThrowIfNull(keys, nameof(keys));

            if (format is not BindingBase bindedFormat)
                throw new ArgumentException("Format must be binding base instance.", nameof(format));


            var multiBinding = new MultiBinding
            {
                Converter = new FuncMultiConverter<string, string>
                (
                    convert: (object[] values) =>
                    {
                        ArgumentNullException
                            .ThrowIfNull(values, nameof(values));

                        if (values.Length == 0)
                            throw new ArgumentException("Values array must be not empty.");

                        values = values[1..];

                        var format = values[0]
                            ?? throw new ArgumentException("Values array zero-index element (format) should be not null.");

                        values[0] = _localizationManager[format.ToString()!].ToString()!;

                        return new StringFormatMultiConverter()
                            .Convert(values, typeof(string), parameter: null!, CultureInfo.CurrentCulture)
                            .ToString()!;
                    },

                    convertBack: null
                )
            };

            multiBinding.Bindings.Add(CreateFictitiousTriggeredBinding(_localizationManager));

            multiBinding.Bindings.Add(bindedFormat);

            foreach (var key in keys)
            {
                if (key is not BindingBase binding)
                {
                    binding = new Binding
                    {
                        Mode = BindingMode.OneTime,
                        Source = key,
                    };
                }

                multiBinding.Bindings.Add(binding);
            }

            return multiBinding;
        }
    }
}
