using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewsWorldwide.Models;
using NewsWorldwide.Data;
using System.Threading.Tasks;

namespace NewsWorldwide.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index(string country = "bg", int currPage = 1)
        {
            try
            {
                var topNewsViewModel = new TopNewsViewModel();
                var list = await GetNews.TopNews(country.ToLower());
                topNewsViewModel.Articles = list.Skip((currPage - 1) * 3).Take(3);
                topNewsViewModel.Country = country.ToLower();
                topNewsViewModel.CurrentPage = currPage;
                topNewsViewModel.TotalNumPages = Calculations.TotalNumPages(list.Count());

                return View(topNewsViewModel);
            }
            catch
            {
                return RedirectToAction("Error", "Error");
            }
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
    }
}
