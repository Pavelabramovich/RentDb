using LilaRent.MobileApp.Core;
using LilaRent.MobileApp.Services;
using System.ComponentModel;


namespace LilaRent.MobileApp.MarkupExtensions;


public abstract class BaseLocalizeExtension : IMarkupExtension<BindingBase>
{
    protected ILocalizationManager _localizationManager;


    public BaseLocalizeExtension(ILocalizationManager localizationManager)
    {
        _localizationManager = localizationManager;
    }

    public BaseLocalizeExtension()
        : this(AppServiceProvider.Instance.GetRequiredService<ILocalizationManager>())
    { }


    public abstract BindingBase ProvideValue(IServiceProvider notCompetentBecauseOfEarlyXamlLoadingServiceProvider);

    object IMarkupExtension.ProvideValue(IServiceProvider notCompetentBecauseOfEarlyXamlLoadingServiceProvider)
    {
        return ProvideValue(notCompetentBecauseOfEarlyXamlLoadingServiceProvider);
    }


    protected abstract class BaseStrategy
    {
        protected readonly ILocalizationManager _localizationManager;


        public BaseStrategy(ILocalizationManager localizationManager)
        {
            _localizationManager = localizationManager;
        }


        private class BindingTrigger : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler? PropertyChanged;


            public BindingTrigger(INotifyPropertyChanged trigger)
            {
                trigger.PropertyChanged += (sender, args) => ChangeValue();
            }

            public byte Value { get; set; }

            private void ChangeValue()
            {
                unchecked
                {
                    Value++;
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }

        protected static Binding CreateFictitiousTriggeredBinding(INotifyPropertyChanged trigger, IValueConverter? converter = null)
        {
            var binding = new Binding
            {
                Mode = BindingMode.OneWay,
                Path = $".Value",
                Source = new BindingTrigger(trigger),
            };

            if (converter is not null)
                binding.Converter = converter;

            return binding;
        }
    }
}
