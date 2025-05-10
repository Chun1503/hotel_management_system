using HotelBusiness.Models;
using HotelRepositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Net;

namespace HotelWebApp.Pages.User
{
    public class DetailsModel : PageModel
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly ILogger<DetailsModel> _logger;
        private readonly HotelDbContext _context;
        private readonly VNPayService _vnPayService;
        private readonly IConfiguration _config;

        public DetailsModel(
             IRoomRepository roomRepository,
        IRoomTypeRepository roomTypeRepository,
        IBookingRepository bookingRepository,
        IServiceRepository serviceRepository,
        ILogger<DetailsModel> logger,
        IConfiguration config,

        HotelDbContext context,
             VNPayService vnPayService)


        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
            _bookingRepository = bookingRepository;
            _serviceRepository = serviceRepository;
            _context = context;
            _logger = logger;
            _vnPayService = vnPayService;
            _config = config;
            BookingInput = new BookingInputModel(); // Khởi tạo BookingInput
        }

        public Room Room { get; set; }
        public List<Room> SimilarRooms { get; set; } = new();
        public List<Service> AvailableServices { get; set; } = new();

        [BindProperty]
        public BookingInputModel BookingInput { get; set; }

        public class BookingInputModel
        {
            [Required(ErrorMessage = "Vui lòng chọn ngày nhận phòng")]
            [Display(Name = "Ngày nhận phòng")]
            public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.Today); // Giá trị mặc định

            [Required(ErrorMessage = "Vui lòng chọn ngày trả phòng")]
            [Display(Name = "Ngày trả phòng")]
            public DateOnly EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddDays(1)); // Giá trị mặc định

            [Required(ErrorMessage = "Vui lòng nhập số lượng khách")]
            [Range(1, 10, ErrorMessage = "Số lượng khách từ 1 đến 10")]
            [Display(Name = "Số lượng khách")]
            public int GuestCount { get; set; } = 2;

            [Required(ErrorMessage = "Vui lòng nhập tên khách hàng")]
            [Display(Name = "Tên khách hàng")]
            public string CustomerName { get; set; } = string.Empty; // Khởi tạo giá trị mặc định

            [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
            [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
            [Display(Name = "Số điện thoại")]
            public string CustomerPhone { get; set; } = string.Empty; // Khởi tạo giá trị mặc định

            [Display(Name = "Ghi chú đặc biệt")]
            public string? Note { get; set; }

            [Display(Name = "Dịch vụ đi kèm")]
            public List<int> SelectedServiceIds { get; set; } = new();
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                Room = await _roomRepository.GetRoomByIdAsync(id);

                if (Room == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy phòng với ID này";
                    return RedirectToPage("/User/Room"); // Chuyển hướng về trang danh sách phòng
                }

                // Lấy các phòng tương tự
                SimilarRooms = (await _roomRepository.GetAvailableRoomsAsync(
                    DateOnly.FromDateTime(DateTime.Today),
                    DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                    Room.IdroomType))
                    .Where(r => r.Idroom != id)
                    .Take(3)
                    .ToList();

                // Lấy dịch vụ
                AvailableServices = (await _serviceRepository.GetAllServicesAsync()).ToList();

                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Đã xảy ra lỗi khi tải thông tin phòng";
                _logger.LogError(ex, "Error loading room details");
                return RedirectToPage("/Error");
            }
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (BookingInput == null)
                {
                    ModelState.AddModelError("", "Dữ liệu đặt phòng không hợp lệ");
                    return await ReloadPageData(id);
                }

                // Tải thông tin phòng
                Room = await _roomRepository.GetRoomByIdAsync(id);
                if (Room == null)
                {                 
                    return RedirectToPage("/User/Room");
                }

                // Kiểm tra ngày hợp lệ
                int numberOfDays = BookingInput.EndDate.DayNumber - BookingInput.StartDate.DayNumber;
                if (numberOfDays <= 0)
                {
                    ModelState.AddModelError("", "Số ngày ở phải lớn hơn 0");
                    return await ReloadPageData(id);
                }

                // Kiểm tra thông tin user
                var accountIdClaim = User.FindFirst("IDAccount");
                if (accountIdClaim == null || !int.TryParse(accountIdClaim.Value, out int accountId))
                {
                    _logger.LogWarning("Không tìm thấy thông tin tài khoản");
                    TempData["ErrorMessage"] = "Vui lòng đăng nhập lại";
                    return RedirectToPage("/Account/Login");
                }

                // Tính toán giá
                decimal totalRoomPrice = Room.Price * numberOfDays;
                decimal totalServicePrice = 0;
                var selectedServiceIds = BookingInput.SelectedServiceIds ?? new List<int>();

                if (selectedServiceIds.Any())
                {
                    var selectedServices = await _serviceRepository.GetServicesByIdsAsync1(selectedServiceIds);
                    if (selectedServices == null || selectedServices.Count != selectedServiceIds.Count)
                    {
                        ModelState.AddModelError("", "Một số dịch vụ không hợp lệ");
                        return await ReloadPageData(id);
                    }
                    totalServicePrice = selectedServices.Sum(s => s.Price) * numberOfDays;
                }

                // Tạo booking
                var booking = new Booking
                {
                    Idroom = id,
                    Idaccount = accountId,
                    StartDate = BookingInput.StartDate,
                    EndDate = BookingInput.EndDate,
                    Deposit = (totalRoomPrice + totalServicePrice),
                    Status = "Pending",
                    Note = BookingInput.Note ?? string.Empty,
                    CustomerName = BookingInput.CustomerName ?? string.Empty,
                    CustomerPhone = BookingInput.CustomerPhone ?? string.Empty
                };

                // Sử dụng transaction
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var result = await _bookingRepository.CreateBookingAsync(booking, selectedServiceIds);
                    if (!result)
                    {
                        throw new Exception("Không thể tạo booking");
                    }

                    await transaction.CommitAsync();

                    // Tạo URL thanh toán VNPay
                    var paymentUrl = HttpContext.Request.Host.Host.Contains("localhost")
     ? $"/mock-vnpay?returnUrl={WebUtility.UrlEncode(_config["VNPay:ReturnUrl"])}&orderId={booking.Idbooking}"
     : _vnPayService.CreatePaymentUrl(new PaymentRequest
     {
         OrderId = booking.Idbooking.ToString(),
         Amount = booking.Deposit,
         OrderInfo = $"Thanh toán đặt phòng #{booking.Idbooking}"
     }, HttpContext);

                    _logger.LogInformation("Payment URL: {Url}", paymentUrl);
                    return Redirect(paymentUrl);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Lỗi khi tạo booking");
                    TempData["ErrorMessage"] = "Đã xảy ra lỗi khi đặt phòng";
                    return await ReloadPageData(id);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi hệ thống khi đặt phòng");
                TempData["ErrorMessage"] = "Đã xảy ra lỗi hệ thống";
                return RedirectToPage("/Error");
            }
        }
    


        private async Task<IActionResult> ReloadPageData(int roomId)
        {
            Room = await _roomRepository.GetRoomByIdAsync(roomId);
            AvailableServices = (await _serviceRepository.GetAllServicesAsync()).ToList();

            if (Room != null)
            {
                SimilarRooms = (await _roomRepository.GetAvailableRoomsAsync(
                    DateOnly.FromDateTime(DateTime.Today),
                    DateOnly.FromDateTime(DateTime.Today.AddDays(1)),
                    Room.IdroomType))
                    .Where(r => r.Idroom != roomId)
                    .Take(3)
                    .ToList();
            }

            return Page();
        }
    }
}