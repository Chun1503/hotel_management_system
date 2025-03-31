using HotelRepositories.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace HotelBusiness.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public LoginModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; } = new();

        public class LoginInputModel
        {
            [Required]
            public string EmailOrUsername { get; set; } = null!;

            [Required]
            public string Password { get; set; } = null!;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _accountRepository.LoginAsync(Input.EmailOrUsername, Input.Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Sai tài khoản hoặc mật khẩu.");
                return Page();
            }

            if (user.Status == "Inactive")
            {
                TempData["InactiveAccount"] = "Tài khoản của bạn chưa được kích hoạt.";
                TempData["UserEmail"] = user.Idemail;
                return Page();
            }

            // Lưu thông tin đăng nhập vào Session
            HttpContext.Session.SetString("UserId", user.Idaccount.ToString());
            HttpContext.Session.SetString("UserName", user.UserName);
            HttpContext.Session.SetString("UserRole", user.Role);

            // Đăng nhập thành công
            Console.WriteLine("Login: " + user.UserName + " - " + user.Role);
            return user.Role switch
            {
                "Admin" => RedirectToPage("/Admin/Dashboard"),
                "Manager" => RedirectToPage("/Manager/Home"),
                _ => RedirectToPage("/Index")
            };
        }

        public IActionResult OnGetLoginWithGoogle()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Page("Login", "GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> OnGetGoogleResponse()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!authenticateResult.Succeeded)
                return RedirectToPage("/Index");

            var claims = authenticateResult.Principal.Identities.FirstOrDefault()?.Claims;
            if (claims == null)
                return RedirectToPage("/Index");

            // Extract user information
            var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name))
                return RedirectToPage("/Index");

            // Check if user exists in the database
            var user = await _accountRepository.GetAccountByEmailAsync(email);
            if (user == null)
            {
                // Create new user
                user = new HotelBusiness.Models.Account
                {
                    Idemail = email,
                    PassWord = Guid.NewGuid().ToString("N").Substring(8),
                    UserName = email,
                    Name = name,
                    Gender = "Unknown",
                    PhoneNumber = "Unknown",
                    Role = "User",
                    Status = "Active",
                };
                await _accountRepository.RegisterAsync(user);
            }

            if (user.Status == "Inactive")
            {
                TempData["InactiveAccount"] = "Tài khoản của bạn chưa được kích hoạt.";
                TempData["UserEmail"] = user.Idemail;
                return RedirectToPage("/Account/Login");
            }

            // Sign in the user with authentication cookie
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, user.Idaccount.ToString()),
                    new Claim(ClaimTypes.Email, user.Idemail),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                }, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            // Lưu thông tin đăng nhập vào Session
            HttpContext.Session.SetString("UserId", user.Idaccount.ToString());
            HttpContext.Session.SetString("UserName", user.UserName);
            HttpContext.Session.SetString("UserRole", user.Role);

            // Đăng nhập thành công
            Console.WriteLine("Login: " + user.UserName + " - " + user.Role);
            return user.Role switch
            {
                "Admin" => RedirectToPage("/Admin/Dashboard"),
                "Manager" => RedirectToPage("/Manager/Home"),
                _ => RedirectToPage("/Index")
            };
        }
    }
}
