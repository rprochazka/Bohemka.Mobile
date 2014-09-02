using Xamarin.Forms;
using System.Threading.Tasks;
using Bohemka.Core.ViewModels;
using Bohemka.Core.Models;

namespace Bohemka.Core.UI.Pages
{	
	public partial class Articles : ContentPage
	{		
		private ArticlesViewModel _viewModel;

		public Articles ()
		{
			//InitializeComponent ();

			this.Content = new Label { 
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Text = "Loading ..."
			};

			Init ();
		}

		private async Task Init()
		{
			_viewModel = new ArticlesViewModel ();

			await _viewModel.InitData ();

			var listView = new ListView {
				ItemsSource = _viewModel.Articles,
				RowHeight = 80
			};					

			listView.ItemTemplate = new DataTemplate (() => {
				var image = new Image();
				image.SetBinding(Image.SourceProperty, "ImagePath");

				var header = new Label();
				header.SetBinding(Label.TextProperty, "Heading");

				var dateModified = new Label();
				dateModified.SetBinding(Label.TextProperty, new Binding (path: "DateModified", stringFormat: "{0:d.M.yyyy}"));

				return new ViewCell{
					View = new StackLayout {
						Orientation = StackOrientation.Horizontal,
						Padding = new Thickness(0, 5),
						Children = {
							image,
							new StackLayout {
								VerticalOptions = LayoutOptions.Center,
								Spacing = 0,
								Children = {
									header,
									dateModified
								}
							}
						}
					}
				};
			});

			listView.SetBinding (ListView.SelectedItemProperty, "SelectedItem");

			listView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
				var selectedItem = (Article)e.SelectedItem;
				Navigation.PushAsync(new ArticleDetail(selectedItem.ArticleId));
			};

			NavigationPage.SetHasNavigationBar (this, true);

			this.Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (
					left: 10, 
					right: 10, 
					bottom: 10, 
					top: Device.OnPlatform (iOS: 20, Android: 0, WinPhone: 0)),
				Children = { 
					listView 
				}
			};					
		}
	}
}

