using HotelBusiness.Models;

namespace HotelRepositories.IRepository
{
    public interface IServiceBookingRepository
    {
        Task<IEnumerable<ServiceBooking>> GetServiceBookingsByBookingIdAsync(int bookingId);
        Task<bool> AddServiceToBookingAsync(ServiceBooking serviceBooking);
        Task<bool> RemoveServiceFromBookingAsync(int serviceBookingId);
    }
}
