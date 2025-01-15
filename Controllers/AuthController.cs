using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RadiologicalSolutionBangladesh.Models;
using RadiologicalSolutionBangladesh.Services;
using System.Threading.Tasks;

namespace RadiologicalSolutionBangladesh.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Username,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await _authService.RegisterAsync(user, model.Password!);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Auth");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _authService.LoginAsync(model.Email!, model.Password!);
                if (user != null)
                {
                    return RedirectToAction("Index", "Profile");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }
    }
}
