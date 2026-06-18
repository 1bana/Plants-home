using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static PLANETS_HOME.Models.ViewModel;
using PLANETS_HOME.Models;
namespace PlanetsHome.Controllers
{
    public class PlantsController : Controller
    {
        private readonly Context _db;
        public PlantsController(Context db) { _db = db; }

        // GET /Plants
        public async Task<IActionResult> Index(string? q, string? category)
        {
            var query = _db.Plants.AsQueryable();

            if (!string.IsNullOrWhiteSpace(q))
                query = query.Where(p =>
                    p.name.Contains(q) ||
                    (p.category != null && p.category.Contains(q)));

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(p => p.category == category);

            var categories = await _db.Plants
                .Where(p => p.category != null)
                .Select(p => p.category!)
                .Distinct().OrderBy(c => c)
                .ToListAsync();

            return View(new CatalogViewModel
            {
                Plants = await query.OrderBy(p => p.name).ToListAsync(),
                SearchQuery = q,
                FilterCategory = category,
                Categories = categories
            });
        }

        // GET /Plants/Detail/5
        public async Task<IActionResult> Detail(int id)
        {
            var plant = await _db.Plants.FindAsync(id);
            if (plant == null) return NotFound();
            return View(plant);
        }

        // POST /Plants/Buy/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy(int id)
        {
            var plant = await _db.Plants.FindAsync(id);
            if (plant == null) return NotFound();

            TempData["BuySuccess"] = $"You successfully bought {plant.name}! 🌿";
            return RedirectToAction("Detail", new { id = id });
        }
    }
}
