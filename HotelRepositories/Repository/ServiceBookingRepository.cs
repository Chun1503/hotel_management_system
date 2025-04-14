using HotelBusiness.Models;
using HotelDataAccess.DAO;
using HotelRepositories.IRepository;

namespace HotelRepositories.Repository
{
    public class ServiceBookingRepository : IServiceBookingRepository
    {
        private readonly ServiceBookingDAO _serviceBookingDAO;

        public ServiceBookingRepository(ServiceBookingDAO serviceBookingDAO)
        {
            _serviceBookingDAO = serviceBookingDAO;
        }

        public Task<IEnumerable<ServiceBooking>> GetServiceBookingsByBookingIdAsync(int bookingId)
        {
            return _serviceBookingDAO.GetServiceBookingsByBookingIdAsync(bookingId);
        }

        public Task<bool> AddServiceToBookingAsync(ServiceBooking serviceBooking)
        {
            return _serviceBookingDAO.AddServiceToBookingAsync(serviceBooking);
        }

        public Task<bool> RemoveServiceFromBookingAsync(int serviceBookingId)
        {
            return _serviceBookingDAO.RemoveServiceFromBookingAsync(serviceBookingId);
        }
    }
}
