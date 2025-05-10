using System;
using System.Threading.Tasks;
using HotelBusiness.Models;
using HotelRepositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HotelWebApp.Pages.User
{
    public class PaymentConfirmModel : PageModel
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBillRepository _billRepository;

        public PaymentConfirmModel(
            IBookingRepository bookingRepository,
            IBillRepository billRepository)
        {
            _bookingRepository = bookingRepository;
            _billRepository = billRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // 1. Lấy thông tin từ URL
            var responseCode = Request.Query["vnp_ResponseCode"].ToString(); // "00" = thành công
            var orderId = Request.Query["vnp_TxnRef"].ToString(); // "24"
            var transactionNo = Request.Query["vnp_TransactionNo"].ToString(); // Số giao dịch

            // 2. Xử lý thanh toán thành công
            if (responseCode == "00" && int.TryParse(orderId, out int bookingId))
            {
                var booking = await _bookingRepository.GetBookingByIdAsync(bookingId);
                if (booking != null && booking.Status != "Paid")
                {
                    var accountId = booking.Idaccount;
                    // Tạo hóa đơn
                    await _billRepository.CreateBillAsync(new Bill
                    {
                        Idbooking = bookingId,
                        Idaccount = accountId,
                        TotalPrice = booking.Deposit, // Sử dụng số tiền từ booking
                        PaymentDate = DateTime.Now,
                        Invoice = transactionNo
                    });

                    // Cập nhật trạng thái
                    booking.Status = "Paid";
                    await _bookingRepository.UpdateBookingAsync(booking);
                }
            }

            // 3. Luôn chuyển hướng về trang lịch sử
            return RedirectToPage("/User/PaymentHistory");
        }
    }
}