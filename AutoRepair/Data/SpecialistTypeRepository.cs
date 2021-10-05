using AutoRepair.Data.Entities;
using AutoRepair.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data
{
    public class SpecialistTypeRepository : GenericRepository<SpecialistType>, ISpecialistTypeRepository
    {
        private readonly DataContext _context;

        public SpecialistTypeRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable GetAllWithUsers()
        {
            return _context.Cars.Include(p => p.User);
        }

        public SpecialistType ToSpecialistType(SpecialistType models, bool isNew)
        {
            return new SpecialistType
            {
                Id = isNew ? 0 : models.Id,
                SpecialistTypeName = models.SpecialistTypeName
            };
        }

        public SpecialistTypeViewModel ToSpecialistTypeViewModel(SpecialistType specialistType)
        {
            return new SpecialistTypeViewModel
            {
                Id = specialistType.Id,
                SpecialistTypeName = specialistType.SpecialistTypeName
            };
        }
    }
}
