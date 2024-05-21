using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Sneat.PL.Helper;
using Sneat.PL.Models;
using System.Security.Claims;

namespace Sneat.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task <IActionResult> Index()
        {
            var Roles = await _roleManager.Roles.ToListAsync();
            return View(Roles);
        }
        [HttpPost]
        public async Task <IActionResult> Create(RoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                var ExcistRole = await _roleManager.RoleExistsAsync(model.Name);
                if (!ExcistRole)
                {
                   await _roleManager.CreateAsync(new IdentityRole(model.Name.Trim()));
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Name", "Role Is Excist");
                    return View("Index",await _roleManager.Roles.ToListAsync());
                }
            }
            return RedirectToAction("Index");
        }

        public async Task <IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }

        public async Task <IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var MappedRole = new AddroleViewModel()
            {
                Name = role.Name,
            };
            return View(MappedRole);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id , AddroleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ExcistRole = await _roleManager.RoleExistsAsync(model.Name);
                if (!ExcistRole)
                {
                    var role = await _roleManager.FindByIdAsync(model.Id);
                    role.Name = model.Name;
                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Name", "Role Is Excist");
                    return View("Index",await _roleManager.Roles.ToListAsync());
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ManageRoles(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if(role is null)
                return NotFound();
            var roleClaimes = _roleManager.GetClaimsAsync(role).Result.Select(c=>c.Value).ToList();
            var allClaimes = Permmisions.GenerateAllPermmision();
            var allPermission = allClaimes.Select(p=>new AddroleViewModel {Name = p }).ToList();
            foreach (var Permission in allPermission)
            {
                if (roleClaimes.Any(c => c == Permission.Name))
                    Permission.IsSelected = true;
            }
            var viewModel = new UserRoleViewModel
            {
                id = id,
                Name = role.Name,
                Roles = allPermission
            };
            return View(viewModel); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ManageRoles(UserRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.id);
            if (role is null)
                return NotFound();
            var roleClaimes = await _roleManager.GetClaimsAsync(role);

            foreach (var claim in roleClaimes)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedClaimes = model.Roles.Where(c=>c.IsSelected).ToList();
            foreach (var item in selectedClaimes)
            {
                await _roleManager.AddClaimAsync(role, new Claim("Permission", item.Name));
                item.Id = model.id;
            }
            return RedirectToAction("Index");
        }
    }
}
