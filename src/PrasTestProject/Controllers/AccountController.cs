using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PrasTestProject.Constants;
using PrasTestProject.Models.ViewModels;

namespace PrasTestProject.Controllers
{
    public class AccountController(
        UserManager<IdentityUser<Guid>> userManager, 
        SignInManager<IdentityUser<Guid>> signInManager) : Controller
    {
        private readonly UserManager<IdentityUser<Guid>> _userManager = userManager;
        private readonly SignInManager<IdentityUser<Guid>> _signInManager = signInManager;

        [HttpGet]
        public IActionResult Login(string returnUrl = "/") => View(new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login attempt");

            return View(model);
        }

        [HttpGet]
        public IActionResult Register(string returnUrl = "/") => View(new RegisterViewModel { ReturnUrl = returnUrl });

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new IdentityUser<Guid> { UserName = model.Login, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, AppRoles.User.ToString());
                await _signInManager.SignInAsync(user, isPersistent: true);

                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
