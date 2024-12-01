using LilaRent.Application.Dto;
using LilaRent.MobileApp.Entities;


namespace LilaRent.MobileApp.Resources.DataTemplates;


public class IsCrownedAnnouncementTemplate : DataTemplateSelector
{
    private readonly DataTemplate _noCrownTemplate;
    private readonly DataTemplate _crownTemplate;


    public IsCrownedAnnouncementTemplate(string commandPath, BindableObject source)
    {
        _noCrownTemplate = new DataTemplate(() =>
        {
            var noCrownView = new AnnouncementTemplate();

            var tapGestureRecognizer = new TapGestureRecognizer();

            var commandBinding = new Binding() { Path = $"BindingContext.{commandPath}", Source = source };
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, commandBinding);

            var commandParameterBinding = new Binding();
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandParameterProperty, commandParameterBinding);

            noCrownView.GestureRecognizers.Add(tapGestureRecognizer);

            return noCrownView;
        });

        _crownTemplate = new DataTemplate(() =>
        {
            var crownView = new CrownedAnnouncementTemplate(commandPath, source);

            var tapGestureRecognizer = new TapGestureRecognizer();

            var commandBinding = new Binding() { Path = $"BindingContext.{commandPath}", Source = source };
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, commandBinding);

            var commandParameterBinding = new Binding();
            tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandParameterProperty, commandParameterBinding);

            crownView.GestureRecognizers.Add(tapGestureRecognizer);

            return crownView;
        });
    }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is not AnnouncementSummaryDto announcement)
            throw new ArgumentException(message: null, nameof(item));

        if (announcement.IsPromoted)
        {
            return _crownTemplate;
        }
        else
        {
            return _noCrownTemplate;
        }
    }
}