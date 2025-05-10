using HotelBusiness.Models;

namespace HotelRepositories.IRepository
{
    public interface IBillRepository
    {
        Task<bool> CreateBillAsync(Bill bill);
        Task<Bill?> GetBillByIdAsync(int billId);
        Task<IEnumerable<Bill>> GetBillsByAccountIdAsync(int accountId);
        Task<bool> UpdateBillAsync(Bill bill);
    }
}
