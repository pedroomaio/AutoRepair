using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data.Entities
{
    public class Car : IEntity
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string RegisterCar { get; set; }
        public DateTime Year { get; set; }
        public string Colour { get; set; }
        public User User { get; set; }
    }
}
