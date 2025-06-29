using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkainRetroMuseumWebApp.Models;
using SkainRetroMuseumWebApp.ViewModels;

namespace SkainRetroMuseumWebApp.Controllers {
 [Authorize] 
    public class AccountController : Controller {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl) {
            LoginViewModel login = new LoginViewModel() {
                ReturnUrl = returnUrl
            };
            return View(login);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel login) {
            if (ModelState.IsValid) {
                AppUser userToLogin = await _userManager.FindByNameAsync(login.Username);
                if (userToLogin != null) {
                    var signInResult = await _signInManager.PasswordSignInAsync(userToLogin, login.Password, false, false);
                    if (signInResult.Succeeded) {
                        return Redirect(login.ReturnUrl ?? "/");
                        }
                }
            }
            ModelState.AddModelError("", "Přihlášení se nezdařilo. Zkontrolujte své uživatelské jméno a heslo.");
            return View(login);
        }
        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();    
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied() {
            return View();
        }
    }
}
