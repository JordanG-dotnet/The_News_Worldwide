using Microsoft.AspNetCore.Mvc;
using NewsWorldwide.Data;
using NewsWorldwide.Models;
using System.Linq;

namespace NewsWorldwide.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult SearchResult(string criteria, string language = "en", int currPage = 1)
        {
            if (criteria != null)
            {
                var searchViewModel = new SearchViewModel();
                searchViewModel.Criteria = criteria;
                searchViewModel.CurrentPage = currPage;
                searchViewModel.Language = language;
                var articlesResponse = GetNews.GetSearchResults(criteria, language).Skip((currPage - 1) * 3).Take(3);
                
                searchViewModel.Articles = articlesResponse;
                return View(searchViewModel);
            }
            else
                return RedirectToAction("Index", "Home");
        }
    }
}