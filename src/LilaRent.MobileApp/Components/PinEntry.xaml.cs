using LilaRent.MobileApp.Converters;
using System;
using System.Globalization;
using System.Text;
using Microsoft.Maui.Controls.Internals;


namespace LilaRent.MobileApp.Components;


public partial class PinEntry : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(PinEntry),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
        nameof(TextColor),
        typeof(Color),
        typeof(PinEntry),
        defaultValue: null
    );

    public static readonly BindableProperty PlaceholderCharacterProperty = BindableProperty.Create(
        nameof(PlaceholderCharacter),
        typeof(char),
        typeof(PinEntry),
        defaultValue: 'X'
    );

    public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
        nameof(PlaceholderColor),
        typeof(Color),
        typeof(PinEntry),
        defaultValue: null
    );

    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
        nameof(FontSize),
        typeof(double),
        typeof(PinEntry),
        defaultValue: Entry.FontSizeProperty.DefaultValue
    );

    public static readonly BindableProperty LengthProperty = BindableProperty.Create(
        nameof(Length),
        typeof(int),
        typeof(PinEntry),
        defaultValue: 5
    );

    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(
        nameof(CharacterSpacing),
        typeof(double),
        typeof(PinEntry),
        defaultValue: Entry.CharacterSpacingProperty.DefaultValue
    );

    public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
        nameof(Keyboard),
        typeof(Keyboard),
        typeof(PinEntry),
        defaultValue: Keyboard.Default
    );


    public PinEntry()
	{
		InitializeComponent();
	}


    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public Color PlaceholderColor
    {
        get => (Color)GetValue(PlaceholderColorProperty);
        set => SetValue(PlaceholderColorProperty, value);
    }

    public char PlaceholderCharacter
    {
        get => (char)GetValue(PlaceholderCharacterProperty);
        set => SetValue(PlaceholderCharacterProperty, value);
    }

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public int Length
    {
        get => (int)GetValue(LengthProperty);
        set => SetValue(LengthProperty, value);
    }

    public double CharacterSpacing
    {
        get => (double)GetValue(CharacterSpacingProperty);
        set => SetValue(CharacterSpacingProperty, value);
    }

    public Keyboard Keyboard
    {
        get => (Keyboard)GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }
}


public class PlaceholderConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        string text = (string)values[0];
        char placeholderChar = (char)values[1];
        int length = (int)values[2];

        var stringBuilder = new StringBuilder();

        if (text is not null)
        {
            foreach (char c in text)
            {
                if (char.IsWhiteSpace(c))
                {
                    stringBuilder.Append(placeholderChar);
                }
                else
                {
                    stringBuilder.Append(' ');
                }
            }
        }

        if (stringBuilder.Length < length)
        {
            int additionPlaceholderLength = length - stringBuilder.Length;
            stringBuilder.Append(placeholderChar, additionPlaceholderLength);
        }

        return stringBuilder.ToString();
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}