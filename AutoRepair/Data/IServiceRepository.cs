using AutoRepair.Data.Entities;
using AutoRepair.Models;
using System.Linq;

namespace AutoRepair.Data
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        public IQueryable GetAllWithUsers();
        public Service ToService(Service models, bool isNew);
        public ServicesViewModel ToServiceViewModel(Service service);
    }
}