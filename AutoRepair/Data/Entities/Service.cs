using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data.Entities
{
    public class Service : IEntity
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
    }
}
