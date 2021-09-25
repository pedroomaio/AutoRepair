using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data.Entities
{
    public class Invoicing : IEntity
    {
        public int Id { get; set; }
        public int MyProperty { get; set; }
        public int RepaiId { get; set; }
        public Repair Repair { get; set; }
        
    }
}
