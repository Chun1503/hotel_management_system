using HotelBusiness.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HotelWebApp.Pages.User
{
    public class ProfileModel : PageModel
    {
        private readonly HotelDbContext _context;

        public ProfileModel(HotelDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public HotelBusiness.Models.Account Account { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var username = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToPage("/Account/Login");
            }

            Account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserName == username);

            if (Account == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // L?y thông tin tài kho?n hi?n t?i t? database
            var existingAccount = await _context.Accounts.FindAsync(Account.Idaccount);
            if (existingAccount == null)
            {
                return NotFound();
            }

            // Ch? c?p nh?t các tr??ng ???c phép thay ??i
            existingAccount.Name = Account.Name;
            existingAccount.Gender = Account.Gender;
            existingAccount.PhoneNumber = Account.PhoneNumber;
            existingAccount.Idfacebook = Account.Idfacebook;
            existingAccount.Bank = Account.Bank;
            existingAccount.BankNumber = Account.BankNumber;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Profile updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(Account.Idaccount))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // Chuy?n h??ng v? chính trang này ?? load l?i d? li?u m?i
            return RedirectToPage();
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.Idaccount == id);
        }
    }
}