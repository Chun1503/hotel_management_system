using System.Threading.Tasks;
using HotelBusiness.Models;
using HotelRepositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HotelWebApp.Pages.User
{
    public class CancelBookingModel : PageModel
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ILogger<CancelBookingModel> _logger;

        public CancelBookingModel(
            IBookingRepository bookingRepository,
            ILogger<CancelBookingModel> logger)
        {
            _bookingRepository = bookingRepository;
            _logger = logger;
        }

        [BindProperty]
        public Booking Booking { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Booking = await _bookingRepository.GetBookingByIdAsync(id);

            if (Booking == null)
            {
                StatusMessage = "Không tìm th?y ??n ??t phòng";
                return RedirectToPage("/User/PaymentHistory");
            }

            // Ki?m tra xem booking có th? h?y không
            if (Booking.Status != "Pending" && Booking.Status != "Confirmed")
            {
                StatusMessage = "??n ??t phòng không th? h?y ? tr?ng thái hi?n t?i";
                return RedirectToPage("/User/PaymentHistory");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id);

            if (booking == null)
            {
                StatusMessage = "Không tìm th?y ??n ??t phòng";
                return RedirectToPage("/User/PaymentHistory");
            }

            try
            {
                // C?p nh?t tr?ng thái
                booking.Status = "Cancelled";
                await _bookingRepository.UpdateBookingAsync(booking);

                StatusMessage = "?ã h?y ??n ??t phòng thành công";
                return RedirectToPage("/User/PaymentHistory");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "L?i khi h?y ??n ??t phòng");
                StatusMessage = "Có l?i x?y ra khi h?y ??n ??t phòng";
                return RedirectToPage("/User/PaymentHistory");
            }
        }
    }
}