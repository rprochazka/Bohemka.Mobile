using Xamarin.Forms;
using Bohemka.Core.ViewModels;

namespace Bohemka.Core.UI.Pages
{	
	public partial class ArticleDetail : ContentPage
	{	
		private readonly string _articleId;

		private ArticleDetailViewModel _viewModel;

		public ArticleDetail (string articleId)
		{
			_articleId = articleId;

			//InitializeComponent ();

			this.Content = new Label { 
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Text = "Loading"
			};

			Init ();
		}

		private async void Init()
		{
			//load the article detail
			_viewModel = new ArticleDetailViewModel ();

			await _viewModel.InitData (_articleId);

			var webView = new WebView { 
				Source = new HtmlWebViewSource { Html = _viewModel.Article.ArticleText},
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			var image = new Image {Source = _viewModel.Article.ImageUrl };

			this.Content = new StackLayout {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (
					left: 10, 
					right: 10, 
					bottom: 10, 
					top: Device.OnPlatform (iOS: 20, Android: 0, WinPhone: 0)),
				Children = { 
					image,
					webView 
				}
			};
		}
	}
}

