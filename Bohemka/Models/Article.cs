using System;

namespace Bohemka.Core.Models
{
	public class Article
	{
		public string ArticleId { get; set;}
		public string Heading { get; set;}
		public string Perex { get; set;}
		public DateTime DateModified { get; set;}
		public Uri ImagePath { get; set;}
	}
}

