using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsWorldwide.Extensions;
using NewsWorldwide.Models;
using System;
using System.Collections.Generic;

namespace NewsWorldwide.Data
{
    public static class GetNews
    {
        private static readonly string apiKey = "10c6e0f03663414faeeb4ec6bc798bf4";
        private static readonly NewsApiClient newsApiClient = new NewsApiClient(apiKey);

        public static IEnumerable<ArticleViewModel> TopNews(string country)
        {
            var cont = Enum.Parse<Countries>(country.ToUpper());
            var topNews = newsApiClient.GetTopHeadlines(new TopHeadlinesRequest()
            { 
                Country = cont
            });
            return topNews.Articles.MapToListArticleViewModel();
        }

        public static IEnumerable<ArticleViewModel> GetSearchResults(string criteria, string language)
        {
            var lang = Enum.Parse<Languages>(language.ToUpper());
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = criteria,
                Language = lang
            });

            return articlesResponse.Articles.MapToListArticleViewModel();
        }
    }
}
