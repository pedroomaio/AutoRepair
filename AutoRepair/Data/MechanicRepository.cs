using AutoRepair.Data.Entities;
using AutoRepair.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data
{
    public class MechanicRepository : GenericRepository<SpecialistType>, IMechanicRepository
    {
        private readonly DataContext _context;

        public MechanicRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddMechanicAsync(MechanicViewModel model)
        {
            var country = await this.GetSpecialistTypeWithMechanicAsync(model.SpecialistTypeId);
            if (country == null)
            {
                return;
            }

            country.Mechanics.Add(new Mechanic { Name = model.Name });
            _context.SpecialistTypes.Update(country);
            await _context.SaveChangesAsync();
        }


        public async Task<int> DeleteMechanicAsync(Mechanic mechanic)
        {
            var country = await _context.SpecialistTypes
                .Where(c => c.Mechanics.Any(ci => ci.Id == mechanic.Id))
                .FirstOrDefaultAsync();
            if (country == null)
            {
                return 0;
            }

            _context.Mechanics.Remove(mechanic);
            await _context.SaveChangesAsync();
            return country.Id;
        }

        public IQueryable GetSpecialistTypeWithMechanic()
        {
            return _context.SpecialistTypes
                .Include(c => c.Mechanics)
                .OrderBy(c => c.SpecialistTypeName);
        }

        public async Task<SpecialistType> GetSpecialistTypeWithMechanicAsync(int id)
        {
            return await _context.SpecialistTypes
                .Include(c => c.Mechanics)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }


        public async Task<int> UpdateMechanicAsync(Mechanic mechanic)
        {
            var country = await _context.SpecialistTypes
                .Where(c => c.Mechanics.Any(ci => ci.Id == mechanic.Id)).FirstOrDefaultAsync();
            if (country == null)
            {
                return 0;
            }

            _context.Mechanics.Update(mechanic);
            await _context.SaveChangesAsync();
            return country.Id;
        }


        public async Task<Mechanic> GetMechanicAsync(int id)
        {
            return await _context.Mechanics.FindAsync(id);
        }


        public async Task<SpecialistType> GetSpecialistTypeAsync(Mechanic mechanic)
        {
            return await _context.SpecialistTypes
                .Where(c => c.Mechanics.Any(ci => ci.Id == mechanic.Id))
                .FirstOrDefaultAsync();
        }


        public IEnumerable<SelectListItem> GetComboSpecialistType()
        {
            var list = _context.SpecialistTypes.Select(c => new SelectListItem
            {
                Text = c.SpecialistTypeName,
                Value = c.Id.ToString()

            }).OrderBy(l => l.Text).ToList();


            list.Insert(0, new SelectListItem
            {
                Text = "(Select a country...)",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboMechanic(int mechanicId)
        {
            var country = _context.SpecialistTypes.Find(mechanicId);
            var list = new List<SelectListItem>();
            if (country != null)
            {
                list = _context.Mechanics.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()

                }).OrderBy(l => l.Text).ToList();


                list.Insert(0, new SelectListItem
                {
                    Text = "(Select a citie...)",
                    Value = "0"
                });

            }

            return list;
        }

    }
}
