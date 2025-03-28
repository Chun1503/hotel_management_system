using HotelBusiness.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HotelDataAccess.DAO
{
    public class AccountDAO
    {
        private readonly HotelDbContext _context;

        public AccountDAO(HotelDbContext context)
        {
            _context = context;
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

    }
}
