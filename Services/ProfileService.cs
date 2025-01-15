using Microsoft.EntityFrameworkCore;
using RadiologicalSolutionBangladesh.Data;
using RadiologicalSolutionBangladesh.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RadiologicalSolutionBangladesh.Services
{
    public interface IProfileService
    {
        Task<Profile?> GetProfileAsync(string userId);
        Task UpdateProfileAsync(Profile profile);
    }

    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext _context;

        public ProfileService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Profile?> GetProfileAsync(string userId)
        {
            if (_context.Profiles != null)
            {
                return await _context.Profiles.FirstOrDefaultAsync(p => p.UserId == userId);
            }
            return null;
        }

        public async Task UpdateProfileAsync(Profile profile)
        {
            if (_context.Profiles != null)
            {
                _context.Profiles.Update(profile);
                await _context.SaveChangesAsync();
            }
        }
    }
}
