using KBDTypeServer.Domain.Entities;
using KBDTypeServer.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace KBDTypeServer.Infrastructure.Services.UserServices
{
    public abstract class DependencyClass
    {
        protected readonly ApplicationDbContext _context;
        protected readonly UserManager<User> _userManager;
        protected readonly SignInManager<User> _signInManager;
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected DependencyClass(
            ApplicationDbContext context,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }
    }
}
