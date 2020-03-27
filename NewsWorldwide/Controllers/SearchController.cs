using Microsoft.AspNetCore.Mvc;
using NewsWorldwide.Data;
using NewsWorldwide.Models;
using System.Linq;
using System.Threading.Tasks;

namespace NewsWorldwide.Controllers
{
    public class SearchController : Controller
    {
        public async  Task<IActionResult> SearchResult(string criteria, string language = "en", int currPage = 1)
        {
            if (criteria != null)
            {
                var searchViewModel = new SearchViewModel();
                searchViewModel.Criteria = criteria;
                searchViewModel.CurrentPage = currPage;
                searchViewModel.Language = language;
                var articlesResponse = await GetNews.GetSearchResults(criteria, language);
                searchViewModel.TotalNumPages = Calculations.TotalNumPages(articlesResponse.Count());
                searchViewModel.Articles = articlesResponse.Skip((currPage - 1) * 3).Take(3);
                return View(searchViewModel);
            }
            else
                return RedirectToAction("Index", "Home");
        }
    }
}