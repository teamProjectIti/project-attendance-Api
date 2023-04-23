using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplication1.Model.User;

namespace WebApplication1.Extension
{
    public static class UserManagerExtensions
    {
        public static async Task<ApplicationUser> FindByEmailWithAddressAsync(this UserManager<ApplicationUser> _context, ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<ApplicationUser> FindByEmailFromClaimsPrinciple(this UserManager<ApplicationUser> _context, ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);

            return await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
        }
    }
}