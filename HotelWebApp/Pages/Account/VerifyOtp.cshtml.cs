using HotelRepositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.Pages.Account
{
    public class VerifyOtpModel : PageModel
    {
        private readonly IAccountRepository _accountRepository;

        public VerifyOtpModel(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required(ErrorMessage = "OTP là bắt buộc.")]
            public string OtpCode { get; set; } = string.Empty;
        }

        public async Task<IActionResult> OnGetAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToPage("/Account/Login");
            }

            var user = await _accountRepository.GetAccountByEmailAsync(email);
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var otpCode = _accountRepository.GenerateOtpCode();
            await _accountRepository.SendOtpEmailAsync(email, otpCode);

            TempData["OtpCode"] = otpCode;
            TempData["UserEmail"] = email;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var otpCode = TempData["OtpCode"] as string;
            if (otpCode == null || Input.OtpCode != otpCode)
            {
                ModelState.AddModelError(string.Empty, "OTP không hợp lệ.");
                return Page();
            }

            var email = TempData["UserEmail"] as string;
            if (email == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var user = await _accountRepository.GetAccountByEmailAsync(email);
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            user.Status = "Active";
            await _accountRepository.UpdateAccountStatusAsync(user.Idaccount, user.Status);

            return RedirectToPage("/Account/Login");
        }
    }
}
