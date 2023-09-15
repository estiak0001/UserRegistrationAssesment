using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Threading.Tasks;
using UserRegistrationAssesment.Models;
using UserRegistrationAssesment.ViewModels;

namespace UserRegistrationAssesment.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager
        {
            get;
        }
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            ResponseModel response = new ResponseModel();

            if (!ModelState.IsValid) return View();

            AppUser newUser = new AppUser
            {
                Email = register.Email,
                UserName = register.Username,
                FullName = register.FullName,
                Mobile = register.Mobile,
                Address = register.Address,
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, register.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            if (result.Succeeded)
            {
                response.IsSuccess = true;
                response.Message = "Registration compleated successfully. Please login to access system.";
                return RedirectToAction("LogIn", response);
            }
            return View();
        }

        public IActionResult LogIn(ResponseModel response)
        {
            ViewBag.IsSuccess = response.IsSuccess;
            ViewBag.Message = response.Message;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginViewModel signIn, string ReturnUrl)
        {
            if (!ModelState.IsValid) return View();

            AppUser user;
            if (signIn.UsernameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(signIn.UsernameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(signIn.UsernameOrEmail);
            }

            if (user == null)
            {
                ModelState.AddModelError("", "You are not registered in this system. Please register first!");
                return View(signIn);
            }
            var result = await _signInManager.PasswordSignInAsync(user, signIn.Password, signIn.RememberMe, true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Something went wrong please try again.");
                return View(signIn);
            }
            if (ReturnUrl != null) return LocalRedirect(ReturnUrl);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(LogIn));
        }

    }
}
