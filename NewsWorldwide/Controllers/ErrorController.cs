using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NewsWorldwide.Models;

namespace NewsWorldwide.Controllers
{
    public class ErrorController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string statusCode, string message)
        {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorStatus = statusCode, Message = message });
        }
    }
} 