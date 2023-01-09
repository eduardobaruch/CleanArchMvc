using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authentication;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IAuthenticate authentication, IHttpContextAccessor httpContextAccessor)
        {
            _authentication = authentication;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model) 
        {
            var result = await _authentication.RegisterUserAsync(model.Email, model.Password);

            if (result)
                return Redirect("/");
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid register attempt (password must be stronger).");
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl) 
        {
            var model = new LoginViewModel 
            { 
                ReturnUrl = returnUrl 
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model) 
        {
            var result = await _authentication.AuthenticateAsync(model.Email, model.Password);

            if(result)
            {
                if (string.IsNullOrEmpty(model.ReturnUrl)) 
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(model.ReturnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout() 
        {
            await _authentication.Logout();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
