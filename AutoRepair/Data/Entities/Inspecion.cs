using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data.Entities
{
    public class Inspecion : IEntity
    {
        public int Id { get; set; }

        [Display(Name = "Prefer Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? PreferDate { get; set; }
        [Required]
        public double? PreferHours { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public IEnumerable<InspecionDetails> Items { get; set; }
        public int ClientId { get; set; }
        public User User { get; set; }
    }
}
