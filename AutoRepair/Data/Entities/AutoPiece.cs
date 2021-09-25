using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data.Entities
{
    public class AutoPiece : IEntity
    {
        public int Id { get; set; }
        public string AutoPieceName { get; set; }
    }
}
