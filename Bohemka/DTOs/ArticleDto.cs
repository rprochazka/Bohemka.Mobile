using System;

namespace Bohemka.Core.DTOs
{
	public class ArticleDto
	{
		public string ArticleId { get; set;}
		public string Heading { get; set;}
		public string Perex { get; set;}
		public DateTime DateUpdated { get; set;}
		// Analysis disable InconsistentNaming
		public string ArticlePhotoPath_w100 { get; set;}
		// Analysis restore InconsistentNaming
	}
}

