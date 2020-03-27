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
            var topNewsViewModel = new TopNewsViewModel();
            var list = await GetNews.TopNews(country.ToLower());
            topNewsViewModel.Articles = list.Skip((currPage - 1) * 3).Take(3);
            topNewsViewModel.Country = country.ToLower();
            topNewsViewModel.CurrentPage = currPage;
            topNewsViewModel.TotalNumPages = Calculations.TotalNumPages(list.Count());

            return View(topNewsViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
