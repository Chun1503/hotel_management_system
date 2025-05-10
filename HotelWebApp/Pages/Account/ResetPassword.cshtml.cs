using HotelDataAccess.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly AccountDAO _accountDAO;

        public ResetPasswordModel(AccountDAO accountDAO)
        {
            _accountDAO = accountDAO;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required(ErrorMessage = "OTP là bắt buộc.")]
            public string OtpCode { get; set; } = string.Empty;

            [Required(ErrorMessage = "Mật khẩu mới là bắt buộc.")]
            [DataType(DataType.Password)]
            public string NewPassword { get; set; } = string.Empty;

            [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc.")]
            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
            public string ConfirmPassword { get; set; } = string.Empty;
        }

        public async Task<IActionResult> OnPostAsync(string email)
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

            var user = await _accountDAO.GetAccountByEmailAsync(email);
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }

            var result = await _accountDAO.ResetPasswordAsync(user.Idemail, Input.NewPassword);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Không thể thay đổi mật khẩu. Mật khẩu cũ không đúng.");
                return Page();
            }

            return RedirectToPage("/Account/Login");
        }
    }
}
