using HotelRepositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public ForgotPasswordModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required(ErrorMessage = "Email hoặc tên đăng nhập là bắt buộc.")]
            public string EmailOrUsername { get; set; } = string.Empty;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _accountRepository.GetAccountByEmailAsync(Input.EmailOrUsername) ??
                       await _accountRepository.GetAccountByUsernameEmailAsync(Input.EmailOrUsername);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Email hoặc tên đăng nhập không tồn tại.");
                return Page();
            }

            var otpCode = _accountRepository.GenerateOtpCode();
            await _accountRepository.SendOtpEmailAsync(user.Idemail, otpCode);

            TempData["OtpCode"] = otpCode;
            TempData["UserEmail"] = user.Idemail;

            return RedirectToPage("/Account/ResetPassword", new { email = user.Idemail });
        }
    }
}
