using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Models
{
    public class InspecionViewModel
    {
        [Display(Name = "Car")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a product.")]
        public int CarId { get; set; }

        [Range(0.0001, double.MaxValue, ErrorMessage = "The quantity must be a positive number.")]
        public double Quantity { get; set; }

        public IEnumerable<SelectListItem> Cars { get; set; }


        [Display(Name = "Brand")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a brand.")]
        public int BrandId { get; set; }


        public IEnumerable<SelectListItem> Brands { get; set; }


        [Display(Name = "Model")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a model.")]
        public int ModelId { get; set; }


        public IEnumerable<SelectListItem> Models { get; set; }
    }
}
