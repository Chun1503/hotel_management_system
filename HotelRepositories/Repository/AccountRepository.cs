using HotelBusiness.Models;
using HotelDataAccess.DAO;
using HotelRepositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
