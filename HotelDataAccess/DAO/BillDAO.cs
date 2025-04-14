using HotelBusiness.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelDataAccess.DAO
{
    public class BillDAO
    {
        private readonly HotelDbContext _context;

        public BillDAO(HotelDbContext context)
        {
            _context = context;
        }

        // Tạo hóa đơn mới
        public async Task<bool> CreateBillAsync(Bill bill)
        {
            _context.Bills.Add(bill);
            await _context.SaveChangesAsync();
            return true;
        }

        // Lấy hóa đơn theo ID
        public async Task<Bill?> GetBillByIdAsync(int billId)
        {
            return await _context.Bills
                .Include(b => b.IdbookingNavigation)
                .FirstOrDefaultAsync(b => b.Idbill == billId);
        }

        // Lấy danh sách hóa đơn của người dùng
        public async Task<IEnumerable<Bill>> GetBillsByAccountIdAsync(int accountId)
        {
            return await _context.Bills
                .Include(b => b.IdbookingNavigation)
                .Where(b => b.IdbookingNavigation.Idaccount == accountId)
                .ToListAsync();
        }

        // Cập nhật hóa đơn
        public async Task<bool> UpdateBillAsync(Bill bill)
        {
            _context.Bills.Update(bill);
            await _context.SaveChangesAsync();
            return true;
        }      



    }
}
