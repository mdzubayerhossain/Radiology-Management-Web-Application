using Microsoft.AspNetCore.Identity;
using RadiologicalSolutionBangladesh.Models;
using System.Threading.Tasks;

namespace RadiologicalSolutionBangladesh.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(ApplicationUser user, string password);
        Task<ApplicationUser?> LoginAsync(string email, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<ApplicationUser?> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (result.Succeeded)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
