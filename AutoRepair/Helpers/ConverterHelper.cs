using AutoRepair.Data.Entities;
using AutoRepair.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Car ToProduct(CarsViewModel model, bool isNew)
        {
            return new Car
            {
                Id = isNew ? 0 : model.Id,
                Modelo = model.Modelo,
                Colour = model.Colour,
                User = model.User
            };
        }

        public CarsViewModel ToProductViewModel(Car Cars)
        {
            return new CarsViewModel
            {
                Id = Cars.Id,
                Modelo = Cars.Modelo,
                Colour = Cars.Colour,
                User = Cars.User
            };
        }

      
    }
}
