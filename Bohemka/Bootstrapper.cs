using AutoMapper;
using System;
using Bohemka.Core.DTOs;
using Bohemka.Core.Models;

namespace Bohemka.Core
{
	public class Bootstrapper
	{
		public void Automapper()
		{
			Mapper.CreateMap<ArticleDto, Article> ()
				.ForMember (dest => dest.DateModified, opt => opt.MapFrom (source => source.DateUpdated))
				.ForMember (dest => dest.ImagePath, opt => opt.ResolveUsing <UriResolver> ()
					.FromMember (source => source.ArticlePhotoPath_w100));

			Mapper.CreateMap<ArticleDetailDto, ArticleDetail> ()				
				.ForMember (dest => dest.ImageUrl, opt => opt.ResolveUsing <UriResolver> ()
					.FromMember (source => source.ArticlePhotoPath_w380));

		}

		private const string WebBaseUri = "http://www.bohemians.cz";
		public class UriResolver : ValueResolver<string, Uri>
		{
			protected override Uri ResolveCore(string source)
			{
				return new Uri(new Uri(WebBaseUri), source);
			}
		}

	}
}

