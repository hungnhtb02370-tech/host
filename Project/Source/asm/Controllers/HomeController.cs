using asm.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace asm.Controllers
{
    public class HomeController : Controller
    {
      

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // HomeController.cs
        public IActionResult Index()
        {
            return File("~/index.html", "text/html");
        }
    }
}
