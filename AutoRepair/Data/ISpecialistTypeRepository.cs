using AutoRepair.Data.Entities;
using AutoRepair.Models;
using System.Linq;

namespace AutoRepair.Data
{
    public interface ISpecialistTypeRepository : IGenericRepository<SpecialistType>
    {
        public IQueryable GetAllWithUsers();
        public SpecialistType ToSpecialistType(SpecialistType models, bool isNew);
        public SpecialistTypeViewModel ToSpecialistTypeViewModel(SpecialistType specialistType);
    }
}
