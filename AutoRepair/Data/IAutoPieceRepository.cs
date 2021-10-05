using AutoRepair.Data.Entities;
using AutoRepair.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data
{
    public interface IAutoPieceRepository : IGenericRepository<AutoPiece>
    {
        public IQueryable GetAllWithUsers();
        public AutoPiece ToAutoPiece(AutoPiece models, bool isNew);
        public AutoPiecesViewModel ToAutoPieceViewModel(AutoPiece autoPiece);
    }
}
