using System.ComponentModel;
using System.Globalization;
using PropertyChangingEventArgs = Microsoft.Maui.Controls.PropertyChangingEventArgs;


namespace LilaRent.MobileApp.Behaviors;


public class NumericCoercingBehavior<TBindableInput> : Behavior<TBindableInput> where TBindableInput : VisualElement, ITextInput
{
    public double MinimumValue { get; set; } = double.NegativeInfinity;
    public double MaximumValue { get; set; } = double.PositiveInfinity;
    public int MaximumDecimalPlaces { get; set; } = int.MaxValue;

    private string _oldText = "";
    private int _position = 0;
    private bool _skipFirst = false;
    private bool _skip = false;


    protected override void OnAttachedTo(TBindableInput bindable)
    {
        base.OnAttachedTo(bindable);

        bindable.PropertyChanging += OnPropertyChanging;
        bindable.PropertyChanged += OnPropertyChanged;

        bindable.Focused += OnFocused;
    }

    protected override void OnDetachingFrom(TBindableInput bindable)
    {
        base.OnDetachingFrom(bindable);

        bindable.PropertyChanging -= OnPropertyChanging;
        bindable.PropertyChanged -= OnPropertyChanged;

        bindable.Focused -= OnFocused;
    }


    private void OnPropertyChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (sender is TBindableInput entry)
        {
            if (args.PropertyName == nameof(ITextInput.Text))
            {
                var newTextValue = entry.Text;
                var oldTextValue = _oldText;

                if (_skip && _skipFirst)
                {
                    return;
                }

                _position = entry.CursorPosition;

                string newText = newTextValue ?? string.Empty;

                if (!IsCorrectNumericInput(newText, CultureInfo.InvariantCulture))
                {
                    entry.Text = oldTextValue;

                    _skipFirst = true;
                    _skip = true;
                }

                entry.CursorPosition = Math.Min(_position, entry.Text.Length);
            }
            else if (args.PropertyName == nameof(ITextInput.CursorPosition))
            {
                int p = entry.CursorPosition;

                if (_skipFirst && p == 0)
                {
                    _skipFirst = false;
                    return;
                }
                else if (!_skipFirst && _skip || _skipFirst)
                {
                    _skipFirst = false;
                    _skip = false;

                    entry.CursorPosition = _position;
                    return;
                }
            }
        }
    }

    private void OnPropertyChanging(object sender, PropertyChangingEventArgs args)
    {
        if (args.PropertyName == nameof(ITextInput.Text))
        {
            var entry = (TBindableInput)sender;

            _position = entry.CursorPosition;
            _oldText = entry.Text;
        }
    }

    private void OnFocused(object? sender, FocusEventArgs e)
    {
        if (sender is TBindableInput entry)
        {
            _oldText = entry.Text;
            _position = entry.CursorPosition;
        }
    }


    private bool IsCorrectNumericInput(string value, CultureInfo culture)
    {
        if (string.IsNullOrEmpty(value)) 
            return true;

        var decimalSeparator = culture.NumberFormat.NumberDecimalSeparator;

        if (value is ['-'] || value == decimalSeparator)
            value += '0';

        if (!double.TryParse(value, culture, out var result) || result < MinimumValue || result > MaximumValue)
        {
            return false;
        }

        int separatorIndex = value.IndexOf(decimalSeparator);

        if (separatorIndex >= 0 && MaximumDecimalPlaces == 0)
        {
            return false;
        }

        int decimalPlaces = separatorIndex >= 0 
            ? value.Length - separatorIndex - 1
            : 0;

        return decimalPlaces <= MaximumDecimalPlaces;
    }
}
