using Microsoft.AspNetCore.Mvc;
using PLANETS_HOME.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;


namespace PLANETS_HOME.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _db;

        public HomeController(ILogger<HomeController> logger, Context db)
        {
            _logger = logger;
            _db = db;
        }

        
        public async Task<IActionResult> Index()
        {
            var r = await _db.Plants
                .OrderBy(p => Guid.NewGuid())
                .Take(3)
            .ToListAsync();
            return View(r);
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




