using LilaRent.MobileApp.ViewModels;


namespace LilaRent.MobileApp.Views;


public partial class UpdateAnnouncementScheduleView : ContentPage
{
    private static readonly TimeSpan DayStart = new(DateTime.MinValue.Ticks);
    private static readonly TimeSpan DayEnd = new(DateTime.MaxValue.Ticks);


    public UpdateAnnouncementScheduleView()
    {
        InitializeComponent();
    }

    public new UpdateAnnouncementScheduleViewModel BindingContext => (UpdateAnnouncementScheduleViewModel)base.BindingContext;


    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        BindingContext.IsBreakEmpty = string.IsNullOrEmpty(BreakEntry.Text);
    }


    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        IsFullDayCheckBox.IsChecked = !IsFullDayCheckBox.IsChecked;
    }

    private void IsFullDayCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs args)
    {
        bool newValue = args.Value;

        if (newValue)
        {
            FromPicker.IsEnabled = false;
            ToPicker.IsEnabled = false;

            FromPicker.Time = DayStart;
            ToPicker.Time = DayEnd;
        }
        else
        {
            FromPicker.IsEnabled = true;
            ToPicker.IsEnabled = true;
        }
    }

    private void FieldEntry_TextChanged(object sender, TextChangedEventArgs args)
    {
        BindingContext.IsBreakEmpty = string.IsNullOrEmpty(args.NewTextValue);
    }
}
