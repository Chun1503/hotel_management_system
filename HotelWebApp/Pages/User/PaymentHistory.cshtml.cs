using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBusiness.Models;
using HotelRepositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HotelWebApp.Pages.User
{
    public class PaymentHistoryModel : PageModel
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IBillRepository _billRepository;
        private readonly ILogger<PaymentHistoryModel> _logger;

        // Danh sách booking và thông báo
        public IEnumerable<Booking> Bookings { get; set; }
        public string ThongBao { get; set; }

        public PaymentHistoryModel(
            IBookingRepository bookingRepository,
            IBillRepository billRepository,
            ILogger<PaymentHistoryModel> logger)
        {
            _bookingRepository = bookingRepository;
            _billRepository = billRepository;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                // Lấy ID người dùng từ session (dạng string)
                var userIdString = HttpContext.Session.GetString("UserId");

                if (string.IsNullOrEmpty(userIdString))
                {
                    _logger.LogError("Không tìm thấy thông tin người dùng trong session");
                    ThongBao = "Vui lòng đăng nhập để xem lịch sử đặt phòng.";
                    return RedirectToPage("/Account/Login");
                }

                // Chuyển đổi từ string sang int
                if (!int.TryParse(userIdString, out int userId))
                {
                    _logger.LogError("ID người dùng không hợp lệ: " + userIdString);
                    ThongBao = "Lỗi hệ thống. Vui lòng đăng nhập lại.";
                    return RedirectToPage("/Account/Login");
                }

                // Lấy danh sách booking theo userId (dạng int)
                Bookings = await _bookingRepository.GetBookingsByAccountIdAsync(userId);

                if (Bookings == null || !Bookings.Any())
                {
                    ThongBao = "Bạn chưa có đơn đặt phòng nào.";
                }

                return Page();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tải lịch sử đặt phòng");
                ThongBao = "Đã xảy ra lỗi khi tải lịch sử đặt phòng. Vui lòng thử lại sau.";
                return Page();
            }
        }
    }
}