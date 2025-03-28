using HotelBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HotelRepositories.IRepository
{
    public interface IAccountRepository
    {
        Task<bool> RegisterAsync(Account account);
        Task<Account?> LoginAsync(string emailOrUsername, string password);
        Task<bool> ChangePasswordAsync(int id, string oldPassword, string newPassword);
    }
}
