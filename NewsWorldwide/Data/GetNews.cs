using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsWorldwide.Extensions;
using NewsWorldwide.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsWorldwide.Data
{
    public static class GetNews
    {
        private static readonly string apiKey = "10c6e0f03663414faeeb4ec6bc798bf4";
        private static readonly NewsApiClient newsApiClient = new NewsApiClient(apiKey);

        public static async Task<IEnumerable<ArticleViewModel>> TopNews(string country)
        {
            try
            {
                var cont = Enum.Parse<Countries>(country.ToUpper());
                var topNews = await newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest()
                {
                    Country = cont
                });
                var list = topNews.Articles.MapToListArticleViewModel();
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task<IEnumerable<ArticleViewModel>> GetSearchResults(string criteria, string language)
        {
            try
            {
                var lang = Enum.Parse<Languages>(language.ToUpper());
                var articlesResponse = await newsApiClient.GetEverythingAsync(new EverythingRequest
                {
                    Q = criteria,
                    Language = lang
                });

                return articlesResponse.Articles.MapToListArticleViewModel();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
