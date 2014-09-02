using Xamarin.Forms;
using Bohemka.Core.UI.Pages;

namespace Bohemka.Core
{
	public class App
	{
		public static Page GetMainPage ()
		{	
//			return new ContentPage { 
//				Content = new Label {
//					Text = "Hello, Forms!",
//					VerticalOptions = LayoutOptions.CenterAndExpand,
//					HorizontalOptions = LayoutOptions.CenterAndExpand,
//				},
//			};

			return new NavigationPage(new Articles ());
		}
	}
}

