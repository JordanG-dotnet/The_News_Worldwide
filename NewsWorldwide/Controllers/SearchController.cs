using Microsoft.AspNetCore.Mvc;
using NewsWorldwide.Data;
using NewsWorldwide.Models;
using System;
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
                try
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
                catch (InvalidOperationException)
                {
                    return RedirectToAction("Error", $"Error", new { statusCode = "404", message = "Specified Language was not found!" });
                }
                catch (ArgumentNullException)
                {
                    return RedirectToAction("Error", "Error", new { statusCode = "503", message = "Server unavailable!" });
                }
                catch (Exception)
                {
                    return RedirectToAction("Error", "Error", new { statusCode = "400", message = "Something went wrong!" });
                }
            }
            else
                return RedirectToAction("Index", "Home");
        }
    }
}