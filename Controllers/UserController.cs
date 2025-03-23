using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AlarmApp.Repositories;

namespace AlarmApp.Controllers
{
	public class UserController : Controller
	{
		private readonly UserRepository _userRepos;

		public UserController(IConfiguration configuration)
		{
			_userRepos = new UserRepository(configuration.GetConnectionString("DefaultConnection"));
		}

		[HttpGet]
		public IActionResult Login()
		{
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Alarm");
            
            return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(string email, string password)
		{
			var user = await _userRepos.GetUserByEmailAsync(email);
			if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
			{
				ModelState.AddModelError("", "Invalid credentials");
				return View();
			}

			var claims = new List<Claim> {
			new Claim(ClaimTypes.Name, user.Email),
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
			var authProperties = new AuthenticationProperties { IsPersistent = true };

			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
										  new ClaimsPrincipal(claimsIdentity),
										  authProperties);

			return RedirectToAction("Index", "Alarm");

			//return View(user);
		}

		[HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "User");
        }

    }
}
