using AutoRepair.Data.Entities;
using AutoRepair.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AutoRepair.Data
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        private readonly DataContext _context;

        public ServiceRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable GetAllWithUsers()
        {
            return _context.Cars.Include(p => p.User);
        }

        public Service ToService(Service models, bool isNew)
        {
            return new Service
            {
                Id = isNew ? 0 : models.Id,
                ServiceName = models.ServiceName
            };
        }

        public ServicesViewModel ToServiceViewModel(Service service)
        {
            return new ServicesViewModel
            {
                Id = service.Id,
                ServiceName = service.ServiceName
            };
        }
    }
}
