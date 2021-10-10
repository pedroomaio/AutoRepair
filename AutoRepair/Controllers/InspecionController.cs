using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoRepair.Data;
using AutoRepair.Helpers;
using AutoRepair.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepair.Controllers
{
    public class InspecionController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IModelRepository _modelRepository;
        private readonly IUserHelper _userHelper;

        public InspecionController(
            ICarRepository carRepository,
             IUserHelper userHelper,
            IModelRepository modelRepository)
        {
            _carRepository = carRepository;
            _userHelper = userHelper;
            _modelRepository = modelRepository;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = new InspecionViewModel();
            if (user != null)
            {
                model.Quantity = 1;
                model.Cars = _carRepository.GetComboCars();


                var brand = await _modelRepository.GetBrandAsync(user.BrandId);
                if (brand != null)
                {
                    var modelRepo = await _modelRepository.GetModelAsync(brand);
                    if (modelRepo != null)
                    {
                        model.ModelId = modelRepo.Id;
                        model.Brands = _modelRepository.GetComboBrands(modelRepo.Id);
                        model.Models = _modelRepository.GetComboModels();
                        model.BrandId = user.BrandId;
                    }
                }
            }

            model.Brands = _modelRepository.GetComboBrands(model.ModelId);
            model.Models = _modelRepository.GetComboModels();

            return View(model);
        }
        //public Task<IActionResult> Index()
        //{
        //    var city = await _modelRepository.GetBrandAsync(user.CityId);
        //    if (city != null)
        //    {
        //        var country = await _modelRepository.GetModelAsync(city);
        //        if (country != null)
        //        {
        //            model.CountryId = country.Id;
        //            model.Cities = _modelRepository.GetComboCities(country.Id);
        //            model.Countries = _modelRepository.GetComboCountries();
        //            model.CityId = user.CityId;
        //        }
        //    }
        //}
    }
}
