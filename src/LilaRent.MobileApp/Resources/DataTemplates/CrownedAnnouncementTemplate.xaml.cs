namespace LilaRent.MobileApp.Resources.DataTemplates;


public partial class CrownedAnnouncementTemplate : ContentView
{
	public CrownedAnnouncementTemplate()
	{
		InitializeComponent();

		ImagesCarouselView.ItemTemplate = new DataTemplate(() =>
		{
			var image = new Image() { Aspect = Aspect.AspectFill };
			image.SetBinding(Image.SourceProperty, new Binding());

			return image;
		});
	}

	public CrownedAnnouncementTemplate(string commandPath, BindableObject source)
	{
		InitializeComponent();

		ImagesCarouselView.ItemTemplate = new DataTemplate(() =>
		{
			var image = new Image() { Aspect = Aspect.AspectFill };

            image.SetBinding(Image.SourceProperty, new Binding());

            var tapGestureRecognizer = new TapGestureRecognizer();

            var commandBinding = new Binding() { Path = $"BindingContext.{commandPath}", Source = source };
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, commandBinding);

            var commandParameterBinding = new Binding("BindingContext", source: this);
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandParameterProperty, commandParameterBinding);

            image.GestureRecognizers.Add(tapGestureRecognizer);

			return image;
        });

	}
}