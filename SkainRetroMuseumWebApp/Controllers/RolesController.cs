using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SkainRetroMuseumWebApp.Controllers {
    public class RolesController : Controller {
        private RoleManager<IdentityRole> _roleManager;
        public RolesController(RoleManager<IdentityRole> roleManager) {
            _roleManager = roleManager;
        }
        public IActionResult Index() {
            return View(_roleManager.Roles);
        }
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name) {
            if (ModelState.IsValid) {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                }
                else {
                    Errors(result);
                }
            }
            return View(name);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id) {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            if (role != null) {
                IdentityResult result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                }
                else {
                    Errors(result);
                }
            }
            else {
                ModelState.AddModelError("", "No role found");
            }
            return View("Index", _roleManager.Roles);
        }
        private void Errors(IdentityResult result) {
            foreach (var error in result.Errors) {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
