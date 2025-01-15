using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RadiologicalSolutionBangladesh.Models;
using System.Threading.Tasks;

namespace RadiologicalSolutionBangladesh.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View(user);
        }
    }
}
