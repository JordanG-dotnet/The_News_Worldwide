using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsWorldwide.Models;
using NewsWorldwide.Data;

namespace NewsWorldwide.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string country = "bg", int currPage = 1)
        {
            var list = GetNews.TopNews(country.ToLower()).Skip((currPage - 1)*3).Take(3);
            list.First().Country = country.ToLower();
            list.First().CurrentPage = currPage;

            return View(list);
        }

        public IActionResult Privacy()
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
