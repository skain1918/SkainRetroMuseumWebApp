using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkainRetroMuseumWebApp.Models;
using SkainRetroMuseumWebApp.ViewModels;

namespace SkainRetroMuseumWebApp.Controllers;
[Authorize(Roles = "kurator")]
public class UsersController : Controller {
    private UserManager<AppUser> _userManager;
    public UsersController(UserManager<AppUser> userManager) {
        _userManager = userManager;
    }
    public IActionResult Index() {
        return View(_userManager.Users);
    }
    public IActionResult Create() {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync(UserViewModel user) {
        if (ModelState.IsValid) {
            var newUser = new AppUser() {
                UserName = user.Username,
                Email = user.Email,
            };
            IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);
            if (result.Succeeded) {
                return RedirectToAction("Index");
            }
            else {
                foreach (var error in result.Errors) {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }
    return View(user); 
    }
}
