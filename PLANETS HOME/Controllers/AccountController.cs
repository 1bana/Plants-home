using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PLANETS_HOME.Models;
using static PLANETS_HOME.Models.ViewModel;



namespace PLANETS_HOME.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

      
            private readonly Context _db;
            public AccountController(Context db) { _db = db; }

            private int? SessionUserId => HttpContext.Session.GetInt32("UserId");

        // ACCOUNT/SIGNUP    
        
            // (GET) 
            public IActionResult SignUp()
            {
                if (SessionUserId != null) return RedirectToAction("Index", "Home");
                return View();
            }

            // POST
            [HttpPost, ValidateAntiForgeryToken]
            public async Task<IActionResult> SignUp(SignUpViewModel model)
            {
                if (!ModelState.IsValid) return View(model);

                if (await _db.Users.AnyAsync(u => u.email == model.Email))
                {
                    ModelState.AddModelError("Email", "This email is already registered.");
                    return View(model);
                }
                if (await _db.Users.AnyAsync(u => u.username == model.Username))
                {
                    ModelState.AddModelError("Username", "This username is already taken.");
                    return View(model);
                }

                var user = new User
                {
                    username = model.Username,
                    email = model.Email,
                    password = model.Password,
                    role = "user"
                };

                _db.Users.Add(user);
                await _db.SaveChangesAsync();

                HttpContext.Session.SetInt32("UserId", user.id);
                HttpContext.Session.SetString("Username", user.username);
                HttpContext.Session.SetString("UserRole", user.role);

                TempData["Success"] = "Account created! Welcome to Planets Home.";
                return RedirectToAction("Index", "Home");
            }

        //ACCOUNT/SIGNOUT
            //  GET 
            public IActionResult SignIn()
            {
                if (SessionUserId != null) return RedirectToAction("Index", "Home");
                return View();
            }

            // POST 
            
            [HttpPost, ValidateAntiForgeryToken]
            public async Task<IActionResult> SignIn(SignInViewModel model)
            {
                if (!ModelState.IsValid) return View(model);

                var user = await _db.Users
                    .FirstOrDefaultAsync(u => u.username == model.username && u.password == model.Password);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid username or password.");
                    return View(model);
                }

                HttpContext.Session.SetInt32("UserId", user.id);
                HttpContext.Session.SetString("Username", user.username);
                HttpContext.Session.SetString("UserRole", user.role);

                // Admin 
                if (user.role == "admin")
                    return RedirectToAction("Dashboard", "Admin");

                return RedirectToAction("Index", "Home");
            }

        //ACCOUTN/SIGNOUT

            //GET
            public IActionResult SignOut()
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }

        //ACOUNT/PROFILE

            //  GET 
            public async Task<IActionResult> Profile()
            {
                if (SessionUserId == null) return RedirectToAction("SignIn");

                var user = await _db.Users.FindAsync(SessionUserId);
                if (user == null) return RedirectToAction("SignIn");

                ViewBag.ChangePasswordModel = new ChangePasswordViewModel();
                return View(user);
            }

            // POST 
            [HttpPost, ValidateAntiForgeryToken]
            public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
            {
                if (SessionUserId == null) return RedirectToAction("SignIn");

                var user = await _db.Users.FindAsync(SessionUserId);
                if (user == null) return RedirectToAction("SignIn");

                if (!ModelState.IsValid)
                {
                    ViewBag.ChangePasswordModel = model;
                    return View("Profile", user);
                }

                if (user.password != model.CurrentPassword)
                {
                    ModelState.AddModelError("CurrentPassword", "Current password is incorrect.");
                    ViewBag.ChangePasswordModel = model;
                    return View("Profile", user);
                }

                user.password = model.NewPassword;
                await _db.SaveChangesAsync();

                TempData["Success"] = "Password changed successfully!";
                return RedirectToAction("Profile");
            }
        }
    
}
