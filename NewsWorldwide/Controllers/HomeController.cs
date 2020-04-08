using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NewsWorldwide.Models;
using NewsWorldwide.Data;
using System.Threading.Tasks;
using System;

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
            catch (InvalidOperationException)
            {
                return RedirectToAction("Error",$"Error", new { statusCode = "404", message = "Specified Country was not found!"});
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
