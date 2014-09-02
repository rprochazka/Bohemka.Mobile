using System.Threading.Tasks;
using Bohemka.Core.Models;
using Bohemka.Core.Services;

namespace Bohemka.Core.ViewModels
{
	public class ArticleDetailViewModel
	{
		public ArticleDetail Article { get; set;}

		public async Task InitData(string articleId)
		{
			var restClient = new RestClient ();
			var articleDetail = await restClient.GetArticleDetail (articleId).ConfigureAwait (false);
			Article = articleDetail;
		}
	}
}

