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

        [Display(Name = "Inspecion Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime InspecionDate { get; set; }
        [Display(Name = "Inspecion Date Start")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? InspecionDateStart { get; set; }
       
        public string InspecionHours { get; set; }
       
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }
        [Display(Name = "Is Active")]
        public bool IsActive{ get; set; }


        
        public IEnumerable<InspecionDetails> Items { get; set; }
        public User User { get; set; }
    }
}
