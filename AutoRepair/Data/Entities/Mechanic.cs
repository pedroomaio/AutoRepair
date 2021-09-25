using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data.Entities
{
    public class Mechanic
    {
        public int Id { get; set; }
        public int Name { get; set; }


        public int CreatedByWhoId { get; set; }
        public User User{ get; set; }
        public int SpecialistId { get; set; }
        public SpecialistType Specialist { get; set; }
    }
}
