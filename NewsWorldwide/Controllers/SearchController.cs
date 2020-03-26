using Microsoft.AspNetCore.Mvc;
using NewsWorldwide.Data;
using NewsWorldwide.Models;

namespace NewsWorldwide.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult SearchResult(string criteria)
        {
            if (criteria != null)
            {
                var searchViewModel = new SearchViewModel();
                searchViewModel.Criteria = criteria;
                var articlesResponse = GetNews.GetSearchResults(criteria);

                searchViewModel.Articles = articlesResponse;
                return View(searchViewModel);
            }
            else
                return RedirectToAction("Index", "Home");
        }
    }
}