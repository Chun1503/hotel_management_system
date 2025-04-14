using HotelBusiness.Models;
using HotelDataAccess.DAO;
using HotelRepositories.IRepository;

namespace HotelRepositories.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ServiceDAO _serviceDAO;

        public ServiceRepository(ServiceDAO serviceDAO)
        {
            _serviceDAO = serviceDAO;
        }

        public Task<List<Service>> GetAllServicesAsync()
        {
            return _serviceDAO.GetAllServicesAsync();
        }

        public Task<Service?> GetServiceByIdAsync(int serviceId)
        {
            return _serviceDAO.GetServiceByIdAsync(serviceId);
        }

        public Task<bool> CreateServiceAsync(Service service)
        {
            return _serviceDAO.CreateServiceAsync(service);
        }

        public Task<bool> UpdateServiceAsync(Service service)
        {
            return _serviceDAO.UpdateServiceAsync(service);
        }

        public Task<bool> DeleteServiceAsync(int serviceId)
        {
            return _serviceDAO.DeleteServiceAsync(serviceId);
        }
        public Task<List<Service>> GetServicesByIdsAsync1(List<int> serviceIds)
        {
            return _serviceDAO.GetServicesByIdsAsync1(serviceIds);
        }


    }
}
