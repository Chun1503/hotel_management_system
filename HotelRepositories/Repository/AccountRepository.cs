using HotelBusiness.Models;
using HotelDataAccess.DAO;
using HotelRepositories.IRepository;

namespace HotelRepositories.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDAO _accountDAO;
        public AccountRepository(AccountDAO accountDAO)
        {
            _accountDAO = accountDAO;
        }
        public Task<bool> ChangePasswordAsync(int id, string oldPassword, string newPassword)
        {
            return _accountDAO.ChangePasswordAsync(id, oldPassword, newPassword);
        }

        public Task<Account?> LoginAsync(string emailOrUsername, string password)
        {
            return _accountDAO.LoginAsync(emailOrUsername, password);
        }

        public Task<bool> RegisterAsync(Account account)
        {
            return _accountDAO.RegisterAsync(account);
        }
        public Task<bool> UpdateProfileAsync(int id, string name, string phone)
        {
            return _accountDAO.UpdateProfileAsync(id, name, phone);
        }
        public Task<bool> UpdateAccountStatusAsync(int id, string status)
        {
            return _accountDAO.UpdateAccountStatusAsync(id, status);
        }

        public Task<Account?> GetAccountByEmailAsync(string email)
        {
            return _accountDAO.GetAccountByEmailAsync(email);
        }
        public string GenerateOtpCode()
        {
            return _accountDAO.GenerateOtpCode();
        }
        public Task SendOtpEmailAsync(string email, string otpCode)
        {
            return _accountDAO.SendOtpEmailAsync(email, otpCode);
        }
        public Task<Account?> GetAccountByUsernameEmailAsync(string username)
        {
            return _accountDAO.GetAccountByUsernameEmailAsync(username);
        }

        public Task<bool> ResetPasswordAsync(string email, string newPassword)
        {
            return _accountDAO.ResetPasswordAsync(email, newPassword);
        }
    }
}
