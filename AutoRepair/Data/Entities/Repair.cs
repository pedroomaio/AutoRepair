using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data.Entities
{
    public class Repair : IEntity
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }
        public bool RepairStatus { get; set; }
        public decimal Price { get; set; }


        public int AutoPieceId { get; set; }
        public AutoPiece AutoPiece { get; set; }
        public Mechanic Mechanic { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int ClientId { get; set; }
        public User User { get; set; }
    }
}
