using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using AutoMapper;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Bohemka.Core.Models;
using Bohemka.Core.DTOs;

namespace Bohemka.Core.Services
{
	public class RestClient
	{			
		public async Task<List<Article>> GetArticles(int homePageId)
		{
			const string relativeUriTemplate = "homepages/{0}?format=json";

			var relativeUri = string.Format (relativeUriTemplate, homePageId);
						
			var articleDtos = await SendRequest<IEnumerable<ArticleDto>> (relativeUri, ParseArticlesJson);

			var articles = await Task.Run(() => 
							Mapper.Map<IEnumerable<ArticleDto>,IEnumerable<Article>> (articleDtos)).ConfigureAwait(false);

			return articles.ToList();
		}

		public async Task<ArticleDetail> GetArticleDetail(string articleId)
		{
			const string relativeUriTemplate = "articles/{0}?format=json";

			var relativeUri = string.Format (relativeUriTemplate, articleId);

			var articleDetailDto = await SendRequest<ArticleDetailDto> (relativeUri, ParseArticleDetailJson);

			var articleDetail = await Task.Run(() => 
				Mapper.Map<ArticleDetailDto, ArticleDetail> (articleDetailDto)).ConfigureAwait(false);

			return articleDetail;
		}


		public async Task<T> SendRequest<T>(string uri, Func<string,T> parseResult) where T:class
		{
			var result = default (T);

			using (var httpClient = CreateClient ()) 
			{
				Debug.WriteLine (string.Format("Sending get request to {0}", uri));
				var response = await httpClient.GetAsync (uri).ConfigureAwait (false);
				if (response.IsSuccessStatusCode) 
				{
					var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
					if (!string.IsNullOrWhiteSpace(json)) 
					{	
						result = await Task.Run (() => parseResult(json)).ConfigureAwait(false);
					}
				}
			}

			return result;
		}

		private IEnumerable<ArticleDto> ParseArticlesJson(string json)
		{
			JObject jjson = JObject.Parse(json);

			var jsonArticleList =
				(from item in jjson ["Homepage"][0]["HomepageItems"]
					select (item ["Article"]).ToString()).ToList();

			var articleDtos = new List<ArticleDto> ();
			foreach (var jsonArticle in jsonArticleList) 
			{
				//yield return JsonConvert.DeserializeObject<ArticleDto>(jsonArticle);
				var articleDto = JsonConvert.DeserializeObject<ArticleDto>(jsonArticle);
				articleDtos.Add (articleDto);
			}						
						
			return articleDtos;
		}

		private ArticleDetailDto ParseArticleDetailJson(string json)
		{
			JObject jjson = JObject.Parse(json);

			//var jsonArticle = (jjson ["Articles"] [0]).ToString ();
				
			//var result = JsonConvert.DeserializeObject<ArticleDetailDto> (jsonArticle);

			var result = jjson.SelectToken ("Articles", false).ToObject<ArticleDetailDto[]> ().FirstOrDefault();

			return result;
		}


		private const string ApiBaseAddress = "http://www.bohemians.cz/api/v1/";
		private HttpClient CreateClient ()
		{
			var httpClient = new HttpClient 
			{ 
				BaseAddress = new Uri(ApiBaseAddress)
			};

			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			return httpClient;
		}
	}
}

