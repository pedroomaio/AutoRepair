using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data.Entities
{
    public class SpecialistType : IEntity
    {
        public int Id { get; set; }
        public string SpecialistTypeName { get; set; }
        public ICollection<Mechanic> Mechanics { get; set; }
    }
}
