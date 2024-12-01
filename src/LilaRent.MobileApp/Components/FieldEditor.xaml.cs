using System.Windows.Input;

namespace LilaRent.MobileApp.Components;

public partial class FieldEditor : ContentView
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(FieldEditor),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );

    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        nameof(Placeholder),
        typeof(string),
        typeof(FieldEditor),
        defaultValue: string.Empty
    );

    public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(
        nameof(MaxLength),
        typeof(int),
        typeof(FieldEditor),
        defaultValue: Entry.MaxLengthProperty.DefaultValue
    );

    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
        nameof(LabelText),
        typeof(string),
        typeof(FieldEditor),
        defaultValue: string.Empty
    );

    public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
        nameof(Keyboard),
        typeof(Keyboard),
        typeof(FieldEditor),
        defaultValue: Keyboard.Default
    );

    public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
        nameof(IsValid),
        typeof(bool),
        typeof(FieldEditor),
        defaultValue: true
    );

    public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
        nameof(ErrorText),
        typeof(string),
        typeof(FieldEditor),
        defaultValue: string.Empty
    );

    public static readonly BindableProperty MaximumFieldHeightRequestProperty = BindableProperty.Create(
        nameof(MaximumFieldHeightRequest),
        typeof(double),
        typeof(FieldEditor),
        defaultValue: Editor.MaximumHeightRequestProperty.DefaultValue
    );


    public FieldEditor()
    {
        InitializeComponent();
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public int MaxLength
    {
        get => (int)GetValue(MaxLengthProperty);
        set => SetValue(MaxLengthProperty, value);
    }

    public string LabelText
    {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }

    public Keyboard Keyboard
    {
        get => (Keyboard)GetValue(KeyboardProperty);
        set => SetValue(KeyboardProperty, value);
    }

    public bool IsValid
    {
        get => (bool)GetValue(IsValidProperty);
        set => SetValue(IsValidProperty, value);
    }

    public string ErrorText
    {
        get => (string)GetValue(ErrorTextProperty);
        set => SetValue(ErrorTextProperty, value);
    }

    public double MaximumFieldHeightRequest
    {
        get => (double)GetValue(MaximumFieldHeightRequestProperty);
        set => SetValue(MaximumFieldHeightRequestProperty, value);
    }
}
