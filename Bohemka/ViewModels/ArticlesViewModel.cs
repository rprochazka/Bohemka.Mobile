using System.Collections.Generic;
using System.Threading.Tasks;
using PropertyChanged;
using Bohemka.Core.Models;
using Bohemka.Core.Services;

namespace Bohemka.Core.ViewModels
{
	[ImplementPropertyChanged]
	public class ArticlesViewModel
	{
		public IEnumerable<Article> Articles {get; private set;}

		public Article SelectedItem { get; set;}

		public async Task InitData()
		{
			var restClient = new RestClient ();
			var articles = await restClient.GetArticles (10).ConfigureAwait (false);
			Articles = articles;
		}
	}


}

