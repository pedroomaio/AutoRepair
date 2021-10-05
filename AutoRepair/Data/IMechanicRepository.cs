using AutoRepair.Data.Entities;
using AutoRepair.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data
{
    public interface IMechanicRepository : IGenericRepository<SpecialistType>
    {
        //public IQueryable GetAllWithUsers();

        Task AddMechanicAsync(MechanicViewModel model);
        Task<int> DeleteMechanicAsync(Mechanic mechanic);
        IQueryable GetSpecialistTypeWithMechanic();
        Task<SpecialistType> GetSpecialistTypeWithMechanicAsync(int id);
        Task<int> UpdateMechanicAsync(Mechanic mechanic);
        Task<Mechanic> GetMechanicAsync(int id);
        Task<SpecialistType> GetSpecialistTypeAsync(Mechanic mechanic);
        IEnumerable<SelectListItem> GetComboSpecialistType();
        IEnumerable<SelectListItem> GetComboMechanic(int mechanicyId);


    }
}
