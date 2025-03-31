using HotelDataAccess.DAO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace HotelWebApp.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly AccountDAO _accountDAO;
        private readonly SendMailService _sendMailService;

        public RegisterModel(AccountDAO accountDAO, SendMailService sendMailService)
        {
            _accountDAO = accountDAO;
            _sendMailService = sendMailService;
            Input = new InputModel();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Tên đầy đủ là bắt buộc.")]
            public string FullName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
            public string Username { get; set; } = string.Empty;

            [Required(ErrorMessage = "Email là bắt buộc.")]
            [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
            [DataType(DataType.Password)]
            public string Password { get; set; } = string.Empty;

            [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc.")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
            public string ConfirmPassword { get; set; } = string.Empty;

            [Required(ErrorMessage = "Giới tính là bắt buộc.")]
            public string Gender { get; set; } = string.Empty;

            [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
            [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
            public string PhoneNumber { get; set; } = string.Empty;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var account = new HotelBusiness.Models.Account
            {
                Name = Input.FullName,
                UserName = Input.Username,
                Idemail = Input.Email,
                PassWord = Input.Password,
                Gender = Input.Gender,
                PhoneNumber = Input.PhoneNumber,
                Status = "Inactive"
            };

            var result = await _accountDAO.RegisterAsync(account);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc email đã tồn tại.");
                return Page();
            }

            //var otpCode = _accountDAO.GenerateOtpCode();

            //await _accountDAO.SendOtpEmailAsync(Input.Email, otpCode);

            //TempData["OtpCode"] = otpCode;
            //TempData["UserEmail"] = Input.Email;

            return RedirectToPage("/Account/VerifyOtp", new { email = Input.Email });
        }
    }
}
