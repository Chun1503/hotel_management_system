using HotelBusiness.Models;

namespace HotelRepositories.IRepository
{
    public interface IAccountRepository
    {
        Task<bool> RegisterAsync(Account account);
        Task<Account?> LoginAsync(string emailOrUsername, string password);
        Task<bool> ChangePasswordAsync(int id, string oldPassword, string newPassword);
        Task<bool> UpdateAccountStatusAsync(int id, string status);
        Task<bool> UpdateProfileAsync(int id, string name, string phone);
        Task<Account?> GetAccountByEmailAsync(string email);
        Task<Account?> GetAccountByUsernameEmailAsync(string username);
        string GenerateOtpCode();
        Task SendOtpEmailAsync(string email, string otpCode);
        Task<bool> ResetPasswordAsync(string email, string newPassword);
    }
}
