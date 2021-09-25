using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data.Entities
{
    public class PostalCode : IEntity
    {
        public int Id { get; set; }   // ID Codigo Postal


        [Required]
        [Display(Name = "Freguesia")]
        [MaxLength(30, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string Parish { get; set; }  // Freguesia


        [Required]
        [Display(Name = "Concelho")]
        [MaxLength(30, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string County { get; set; }  // Concelho


        [Required]
        [Display(Name = "Distrito")]
        [MaxLength(20, ErrorMessage = "The field {0} can contain {1} characters length.")]
        public string District { get; set; }    // Distrito
    }
}
