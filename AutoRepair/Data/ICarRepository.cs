using AutoRepair.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace AutoRepair.Data
{
    public interface ICarRepository : IGenericRepository<Car>
    {
        public IQueryable GetAllWithUsers();


        IEnumerable<SelectListItem> GetComboProducts();
    }
}
