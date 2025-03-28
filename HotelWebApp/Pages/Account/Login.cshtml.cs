using HotelBusiness.Models;
using HotelRepositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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

            // Lưu thông tin đăng nhập vào Session
            HttpContext.Session.SetString("UserId", user.Idaccount.ToString());
            HttpContext.Session.SetString("UserName", user.UserName);
            HttpContext.Session.SetString("UserRole", user.Role);

            // Chuyển hướng theo Role
            return user.Role switch
            {
                "Admin" => RedirectToPage("/Admin/Dashboard"),
                "Manager" => RedirectToPage("/Manager/Home"),
                _ => RedirectToPage("/User/Home")
            };
        }
    }
}
