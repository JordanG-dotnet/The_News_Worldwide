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
        //FOR THE apiKey PLEASE GO TO NewsAPI.org and REGISTER FOR YOUR VERY OWN KEY
        private static readonly string apiKey = "your key here";
        private static readonly NewsApiClient newsApiClient = new NewsApiClient(apiKey);

        public static async Task<IEnumerable<ArticleViewModel>> TopNews(string country)
        {
            try
            {
                var cont = Enum.Parse<Countries>(country.ToUpper());
                var topNews = await newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest()
                {
                    Country = cont,
                    PageSize = 100
                });
                var list = topNews.Articles.MapToListArticleViewModel();
                return list;
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException();
            }
            catch (Exception)
            {
                throw new InvalidOperationException();
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
                    Language = lang,
                    PageSize = 100
                });

                return articlesResponse.Articles.MapToListArticleViewModel();
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException();
            }
            catch (Exception)
            {
                throw new InvalidOperationException();
            }
        }
    }
}
