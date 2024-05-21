using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sneat.DAL.Entity;
using Sneat.PL.Models;

namespace Sneat.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task <IActionResult> Index()
        {
            var users = await _userManager.Users.Select(u => new UserViewModel()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                Email = u.Email,
                Roles =_userManager.GetRolesAsync(u).Result
            }
            ).ToArrayAsync();
            return View(users);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var AllRoles = await _roleManager.Roles.ToListAsync();
            var viewModel = new UserRoleViewModel()
            {
                id = user.Id,
                Name = user.FirstName,
                Roles = AllRoles.Select(r => new AddroleViewModel()
                {
                    Id = r.Id,
                    Name = r.Name,
                    IsSelected = _userManager.IsInRoleAsync(user, r.Name).Result
                }).ToList(),
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserRoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.id);
            var userRole = await _userManager.GetRolesAsync(user);
            foreach (var role in model.Roles)
            {
                if(userRole.Any(r=>r==role.Name) && !role.IsSelected)
                {
                    await _userManager.RemoveFromRoleAsync(user,role.Name);
                }
                if (!userRole.Any(r => r == role.Name) && role.IsSelected)
                {
                   await _userManager.AddToRoleAsync(user,role.Name);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
