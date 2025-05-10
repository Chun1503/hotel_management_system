using HotelBusiness.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelDataAccess.DAO
{
    public class AccountDAO
    {
        private readonly HotelDbContext _context;
        private readonly SendMailService _sendMailService;

        public AccountDAO(HotelDbContext context, SendMailService sendMailService)
        {
            _context = context;
            _sendMailService = sendMailService;
        }

        // Đăng ký
        public async Task<bool> RegisterAsync(Account account)
        {
            var existingAccount = await _context.Accounts
                .AnyAsync(a => a.Idemail == account.Idemail || a.UserName == account.UserName);

            if (existingAccount) return false;

            account.PassWord = BCrypt.Net.BCrypt.HashPassword(account.PassWord);
            account.Role = "User";
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return true;
        }

        // Đăng nhập
        public async Task<Account?> LoginAsync(string emailOrUsername, string password)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Idemail == emailOrUsername || a.UserName == emailOrUsername);

            if (account != null && BCrypt.Net.BCrypt.Verify(password, account.PassWord))
            {
                return account;
            }

            return null;
        }

        // Cập nhật hồ sơ
        public async Task<bool> UpdateProfileAsync(int id, string name, string phone)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return false;

            account.Name = name;
            account.PhoneNumber = phone;
            await _context.SaveChangesAsync();
            return true;
        }

        // Đổi mật khẩu
        public async Task<bool> ChangePasswordAsync(int id, string oldPassword, string newPassword)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null || !BCrypt.Net.BCrypt.Verify(oldPassword, account.PassWord))
                return false;

            account.PassWord = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _context.SaveChangesAsync();
            return true;
        }

        // Cập nhật trạng thái
        public async Task<bool> UpdateAccountStatusAsync(int id, string status)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account == null) return false;

            account.Status = status;
            await _context.SaveChangesAsync();
            return true;
        }

        // Get account by email
        public async Task<Account?> GetAccountByEmailAsync(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Idemail == email);
        }

        // Generate OTP code
        public string GenerateOtpCode()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        // Send OTP email
        public async Task SendOtpEmailAsync(string email, string otpCode)
        {
            var subject = "OTP Verification";
            var htmlMessage = $"Your OTP code is {otpCode}";

            await _sendMailService.SendEmailAsync(email, subject, htmlMessage);
        }
        public async Task<Account?> GetAccountByUsernameEmailAsync(string username)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(a => a.Idemail == username || a.UserName == username);
        }
        // Reset mật khẩu
        public async Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Idemail == email);
            if (account == null) return false;

            account.PassWord = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
