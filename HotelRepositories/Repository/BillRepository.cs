using HotelBusiness.Models;
using HotelDataAccess.DAO;
using HotelRepositories.IRepository;

namespace HotelRepositories.Repository
{
    public class BillRepository : IBillRepository
    {
        private readonly BillDAO _billDAO;

        public BillRepository(BillDAO billDAO)
        {
            _billDAO = billDAO;
        }

        public Task<bool> CreateBillAsync(Bill bill)
        {
            return _billDAO.CreateBillAsync(bill);
        }

        public Task<Bill?> GetBillByIdAsync(int billId)
        {
            return _billDAO.GetBillByIdAsync(billId);
        }

        public Task<IEnumerable<Bill>> GetBillsByAccountIdAsync(int accountId)
        {
            return _billDAO.GetBillsByAccountIdAsync(accountId);
        }

        public Task<bool> UpdateBillAsync(Bill bill)
        {
            return _billDAO.UpdateBillAsync(bill);
        }
    }
}
