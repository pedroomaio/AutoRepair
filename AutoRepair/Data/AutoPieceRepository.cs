using AutoRepair.Data.Entities;
using AutoRepair.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data
{
    public class AutoPieceRepository : GenericRepository<AutoPiece>, IAutoPieceRepository
    {
        private readonly DataContext _context;

        public AutoPieceRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable GetAllWithUsers()
        {
            return _context.Cars.Include(p => p.User);
        }

        public AutoPiece ToAutoPiece(AutoPiece models, bool isNew)
        {
            return new AutoPiece
            {
                Id = isNew ? 0 : models.Id,
                AutoPieceName = models.AutoPieceName
            };
        }

        public AutoPiecesViewModel ToAutoPieceViewModel(AutoPiece autoPiece)
        {
            return new AutoPiecesViewModel
            {
                Id = autoPiece.Id,
                AutoPieceName = autoPiece.AutoPieceName
            };
        }
    }
}