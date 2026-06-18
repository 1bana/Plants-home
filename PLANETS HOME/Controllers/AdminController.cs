using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLANETS_HOME.Models;
using static PLANETS_HOME.Models.ViewModel;

namespace PlanetsHome.Controllers
{
    public class AdminController : Controller
    {
        private readonly Context _db;
        public AdminController(Context db) { _db = db; }

        private bool IsAdmin =>
            HttpContext.Session.GetString("UserRole") == "admin";

        // ── GET /Admin/Dashboard ──────────────────────────────────
        public async Task<IActionResult> Dashboard()
        {
            if (!IsAdmin) return RedirectToAction("SignIn", "Account");

            ViewBag.PlantCount = await _db.Plants.CountAsync();
            ViewBag.UserCount = await _db.Users.CountAsync(u => u.role == "user");
            ViewBag.Categories = await _db.Plants
                .Where(p => p.category != null)
                .Select(p => p.category!)
                .Distinct().CountAsync();

            return View(await _db.Plants.OrderBy(p => p.name).ToListAsync());
        }

        // ── GET /Admin/AddPlant ───────────────────────────────────
        public IActionResult AddPlant()
        {
            if (!IsAdmin) return RedirectToAction("SignIn", "Account");
            return View(new PlantFormViewModel());
        }

        // ── POST /Admin/AddPlant ──────────────────────────────────
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPlant(PlantFormViewModel model)
        {
            if (!IsAdmin) return RedirectToAction("SignIn", "Account");
            if (!ModelState.IsValid) return View(model);

            _db.Plants.Add(new Plant
            {
                name = model.Name,
                category = model.Category,
                light = model.LightNeed,
                water = model.WaterNeed,
                humidity = model.Humidity,
                description = model.Description,
                caretips = model.CareTips
            });
            await _db.SaveChangesAsync();

            TempData["Success"] = $"'{model.Name}' added successfully!";
            return RedirectToAction("Dashboard");
        }

        // ── GET /Admin/EditPlant/5 ────────────────────────────────
        public async Task<IActionResult> EditPlant(int id)
        {
            if (!IsAdmin) return RedirectToAction("SignIn", "Account");

            var plant = await _db.Plants.FindAsync(id);
            if (plant == null) return NotFound();

            return View(new PlantFormViewModel
            {
                Id = plant.id,
                Name = plant.name,
                Category = plant.category,
                LightNeed = plant.light,
                WaterNeed = plant.water,
                Humidity = plant.humidity,
                Description = plant.description,
                CareTips = plant.caretips
            });
        }

        // ── POST /Admin/EditPlant ─────────────────────────────────
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPlant(PlantFormViewModel model)
        {
            if (!IsAdmin) return RedirectToAction("SignIn", "Account");
            if (!ModelState.IsValid) return View(model);

            var plant = await _db.Plants.FindAsync(model.Id);
            if (plant == null) return NotFound();

            plant.name = model.Name;
            plant.category = model.Category;
            plant.light = model.LightNeed;
            plant.water = model.WaterNeed;
            plant.humidity = model.Humidity;
            plant.description = model.Description;
            plant.caretips = model.CareTips;

            await _db.SaveChangesAsync();

            TempData["Success"] = $"'{plant.name}' updated successfully!";
            return RedirectToAction("Dashboard");
        }

        // ── POST /Admin/DeletePlant ───────────────────────────────
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePlant(int id)
        {
            if (!IsAdmin) return RedirectToAction("SignIn", "Account");

            var plant = await _db.Plants.FindAsync(id);
            if (plant != null)
            {
                _db.Plants.Remove(plant);
                await _db.SaveChangesAsync();
                TempData["Success"] = $"'{plant.name}' deleted.";
            }
            return RedirectToAction("Dashboard");
        }
    }
}