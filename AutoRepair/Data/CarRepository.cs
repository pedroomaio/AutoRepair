using AutoRepair.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data
{
    public class CarRepository : GenericRepository<Car>, ICarRepository
    {
        private readonly DataContext _context;

        public CarRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public IQueryable GetAllWithUsers()
        {
            return _context.Cars.Include(p => p.User);
        }

        public IEnumerable<SelectListItem> GetComboProducts()
        {

            var list = _context.Cars.Select(p => new SelectListItem
            {
                Text = p.Colour,
                Value = p.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a product...)",
                Value = "0"
            });

            return list;

        }
    }
}
