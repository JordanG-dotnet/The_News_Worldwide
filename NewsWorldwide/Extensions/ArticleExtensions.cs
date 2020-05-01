using NewsAPI.Models;
using NewsWorldwide.Models;
using System.Collections.Generic;
using System.Linq;

namespace NewsWorldwide.Extensions
{
    public static class ArticleExtensions
    {
        public static ArticleViewModel MapToArticleViewModel(this Article article)
        {
            var articleViewModel = new ArticleViewModel()
            {
                Author = article.Author,
                PublishedAt = article.PublishedAt,
                Description = article.Description,
                URL = article.Url,
                URLToImage = article.UrlToImage,
                Title = article.Title
            };

            return articleViewModel;
        }

        public static IEnumerable<ArticleViewModel> MapToListArticleViewModel(this IEnumerable<Article> articles)
        {
            var articleViewModelList = articles.Select(art => art.MapToArticleViewModel()).ToList();
            return articleViewModelList;
        }
    }
}
