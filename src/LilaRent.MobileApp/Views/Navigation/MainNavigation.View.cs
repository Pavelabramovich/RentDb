using LilaRent.MobileApp.Core;

namespace LilaRent.MobileApp.Views;


public class MainNavigationView : NavigationView
{
	private readonly Style _navLabelStyle;


	public MainNavigationView(IServiceProvider serviceProvider)
		: base(serviceProvider)
	{
		BackButtonStyle = (Style)Microsoft.Maui.Controls.Application.Current.Resources["BackButtonStyle"];
        _navLabelStyle = (Style)Microsoft.Maui.Controls.Application.Current.Resources["PrimaryHeaderDarkStyle"];
    }

	protected override EnhancedControlTemplate CreateEnhancedTemplate(ControlTemplate template, Page? currentPage = null)
	{
		return new StyledNavigationBarTemplate(template, this, _navLabelStyle, currentPage);
	}


    protected class StyledNavigationBarTemplate : NavigationBarTemplate
	{
		private readonly Style _navLabelStyle;


		public StyledNavigationBarTemplate(ControlTemplate controlTemplate, NavigationView navigationView, Style titleLabelStyle, Page? currentPage = null)
			: base(controlTemplate, navigationView, currentPage)
		{ 
			_navLabelStyle = titleLabelStyle;
		}

        protected override View CreateTitleView()
        {
            var titleView = base.CreateTitleView();

			if (titleView is ContentView { Content: Label { Style: null } titleLabel })
			{
				titleLabel.Style = _navLabelStyle;
				titleLabel.VerticalTextAlignment = TextAlignment.Center;
				titleLabel.HorizontalTextAlignment = TextAlignment.Center;
				titleLabel.Margin = new Thickness(16, 16, 16, 0);
			}

			return titleView;
        }

        public override StyledNavigationBarTemplate AddCurrentPage(Page currentPage)
        {
            return new StyledNavigationBarTemplate(Template, _navigationView, _navLabelStyle, currentPage);
        }
    }
}