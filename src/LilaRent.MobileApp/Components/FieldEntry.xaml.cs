using Font = Microsoft.Maui.Font;


namespace LilaRent.MobileApp.Components;


public partial class FieldEntry : ContentView, ITextInput
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(FieldEntry),
        defaultValue: string.Empty,
        defaultBindingMode: BindingMode.TwoWay
    );

    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        nameof(Placeholder),
        typeof(string),
        typeof(FieldEntry),
        defaultValue: string.Empty
    );

    public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create(
        nameof(MaxLength),
        typeof(int),
        typeof(FieldEntry),
        defaultValue: Entry.MaxLengthProperty.DefaultValue
    );

    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
        nameof(LabelText),
        typeof(string),
        typeof(FieldEntry),
        defaultValue: string.Empty
    );

    public static readonly BindableProperty KeyboardProperty = BindableProperty.Create(
        nameof(Keyboard),
        typeof(Keyboard),
        typeof(FieldEntry),
        defaultValue: Keyboard.Default
    );

    public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
        nameof(IsPassword),
        typeof(bool),
        typeof(FieldEntry),
        defaultValue: false
    );

    public static readonly BindableProperty IsPasswordButtonVisibleProperty = BindableProperty.Create(
        nameof(IsPasswordButtonVisible),
        typeof(bool),
        typeof(FieldEntry),
        defaultValue: false
    );

    public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
        nameof(ErrorText),
        typeof(string),
        typeof(FieldEntry),
        defaultValue: string.Empty
    );

    public static readonly BindableProperty IsValidProperty = BindableProperty.Create(
        nameof(IsValid),
        typeof(bool),
        typeof(FieldEntry),
        defaultValue: true
    );

    public static readonly BindableProperty CursorPositionProperty = BindableProperty.Create(
        nameof(CursorPosition),
        typeof(int),
        typeof(FieldEntry),
        defaultValue: (int)Entry.CursorPositionProperty.DefaultValue,
        defaultBindingMode: BindingMode.TwoWay
    );


    public FieldEntry()
    {
        InitializeComponent();
        Entry.TextChanged += Entry_TextChanged;
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

    public bool IsPassword
    { 
        get => (bool)GetValue(IsPasswordProperty);
        set => SetValue(IsPasswordProperty, value);
    }

    public bool IsPasswordButtonVisible
    {
        get => (bool)GetValue(IsPasswordButtonVisibleProperty);
        set => SetValue(IsPasswordButtonVisibleProperty, value);
    }

    public string ErrorText
    {
        get => (string)GetValue(ErrorTextProperty);
        set => SetValue(ErrorTextProperty, value);
    }

    public bool IsValid
    {
        get => (bool)GetValue(IsValidProperty);
        set => SetValue(IsValidProperty, value);
    }

    public int CursorPosition
    {
        get => (int)GetValue(CursorPositionProperty);
        set => SetValue(CursorPositionProperty, value);
    }

    public event EventHandler<TextChangedEventArgs>? TextChanged;


    public bool IsTextPredictionEnabled => Entry.IsTextPredictionEnabled;
    public bool IsSpellCheckEnabled => Entry.IsSpellCheckEnabled;
    public bool IsReadOnly => Entry.IsReadOnly;

    public int SelectionLength 
    { 
        get => Entry.SelectionLength; 
        set => Entry.SelectionLength = value; 
    }

    public Color TextColor
    {
        get => Entry.TextColor;
        set => throw new NotSupportedException();
    }

    public Font Font => (Entry as ITextInput).Font;

    public double CharacterSpacing => Entry.CharacterSpacing;

    public Color PlaceholderColor 
    {
        get => Entry.PlaceholderColor; 
        set => throw new NotSupportedException();
    }


    private void EyeButton_Clicked(object sender, EventArgs e)
    {
        IsPassword = !IsPassword;
    }

    private void Entry_TextChanged(object? sender, TextChangedEventArgs args)
    {
        TextChanged?.Invoke(this, args);
    }
}
