﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data.Entities
{
    public class Mechanic : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public int SpecialistId { get; set; }
    }
}
