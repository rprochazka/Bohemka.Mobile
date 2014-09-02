using System;

namespace Bohemka.Core.DTOs
{
	public class ArticleDetailDto
	{
		public string Heading {get; set;}
		public string Perex {get; set;}
		public string Permalink {get; set;}
		public string ArticleText {get; set;}
		// Analysis disable InconsistentNaming
		public string ArticlePhotoPath_w380 {get; set;}
		// Analysis restore InconsistentNaming
	}
}

