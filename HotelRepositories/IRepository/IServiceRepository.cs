using HotelBusiness.Models;

namespace HotelRepositories.IRepository
{
    public interface IServiceRepository
    {
        Task<List<Service>> GetAllServicesAsync();
        Task<Service?> GetServiceByIdAsync(int serviceId);
        Task<bool> CreateServiceAsync(Service service);
        Task<bool> UpdateServiceAsync(Service service);
        Task<bool> DeleteServiceAsync(int serviceId);
        Task<List<Service>> GetServicesByIdsAsync1(List<int> serviceIds);
    }
}
