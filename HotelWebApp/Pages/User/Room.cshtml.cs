using HotelBusiness.Models;
using HotelRepositories.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelWebApp.Pages.User
{
    public class RoomModel : PageModel
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomTypeRepository _roomTypeRepository;
        private readonly ILogger<RoomModel> _logger;

        public RoomModel(
            IRoomRepository roomRepository,
            IRoomTypeRepository roomTypeRepository,
            ILogger<RoomModel> logger)
        {
            _roomRepository = roomRepository;
            _roomTypeRepository = roomTypeRepository;
            _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public DateOnly? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateOnly? EndDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? RoomTypeId { get; set; }

        public List<Room> AvailableRooms { get; set; } = new();
        public List<SelectListItem> RoomTypes { get; set; } = new();
        public string CurrentSearchRange { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                // L?y danh sách lo?i phòng
                var roomTypes = await _roomTypeRepository.GetAllRoomTypesAsync();
                RoomTypes = roomTypes.Select(rt => new SelectListItem
                {
                    Value = rt.IdroomType.ToString(),
                    Text = rt.TypeName,
                    Selected = rt.IdroomType == RoomTypeId
                }).ToList();

                // Thêm option "T?t c?"
                RoomTypes.Insert(0, new SelectListItem("All Room", ""));

                // X? lý ngày m?c ??nh
                var today = DateOnly.FromDateTime(DateTime.Today);
                StartDate ??= today;
                EndDate ??= today.AddDays(1);

                // ??m b?o EndDate >= StartDate
                if (EndDate < StartDate)
                {
                    EndDate = StartDate.Value.AddDays(1);
                }

                // Format hi?n th?
                CurrentSearchRange = StartDate == EndDate
                    ? StartDate.Value.ToString("dd/MM/yyyy")
                    : $"{StartDate.Value.ToString("dd/MM/yyyy")} - {EndDate.Value.ToString("dd/MM/yyyy")}";

                // L?y danh sách phòng tr?ng
                AvailableRooms = await _roomRepository.GetAvailableRoomsAsync(
                    StartDate.Value,
                    EndDate.Value,
                    RoomTypeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading available rooms");
                TempData["ErrorMessage"] = "?ã có l?i x?y ra khi t?i danh sách phòng";
            }
        }
    }
}