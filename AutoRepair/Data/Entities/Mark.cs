using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data.Entities
{
    public class Mark : IEntity
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }
        public bool RepairStatus { get; set; }
        public int RepairId { get; set; }
        public Repair Repair { get; set; }
        public int MechanicId { get; set; }
        public Mechanic Mechanic { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
