using AutoRepair.Data;
using AutoRepair.Data.Entities;
using AutoRepair.Helpers;
using AutoRepair.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Controllers
{
    public class CarsController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IUserHelper _userHelper;
        //private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;
        private readonly IModelRepository _modelRepository;

        public CarsController(
            ICarRepository carRepository,
            IUserHelper userHelper,
            //IBlobHelper blobHelper)
            IConverterHelper converterHelper,
            IModelRepository modelRepository)
        {
            _carRepository = carRepository;
            _userHelper = userHelper;
            //_blobHelper = blobHelper;
            _converterHelper = converterHelper;
            _modelRepository = modelRepository;
        }

        // GET: Products

        [Authorize(Roles = "Admin,Customer")]

        public IActionResult Index()
        {

            if (User.IsInRole("Admin"))
            {
                return View(_carRepository.GetAll().OrderBy(p => p.Model));
            }
            else
            {
                return View(_carRepository.GetAll().Where(p => p.User.Email == User.Identity.Name).OrderBy(p => p.Model));
            }
        }

        // GET: Products/Details/5
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("CarNotFound");
            }

            var car = await _carRepository.GetByIdAsync(id.Value);
            if (car == null)
            {
                return new NotFoundViewResult("CarNotFound");
            }

            foreach (var item in _carRepository.GetAll().Where(p => p.User.Email == User.Identity.Name))
            {
                if (id == item.Id)
                {
                    var model = _converterHelper.ToCarsViewModel(car);
                    return View(model);
                }
            }

            return View(car);
        }


        //// GET: Products/Create
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Create()
        {

            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = new CarsViewModel();
            if (user != null)
            {
                model.Quantity = 1;
                model.Cars = _carRepository.GetComboCars();


                var brand = await _modelRepository.GetBrandWithUserAsync(user.Id);
                if (brand != null)
                {
                    var modelRepo = await _modelRepository.GetModelAsync(brand);
                    if (modelRepo != null)
                    {
                        model.ModelId = modelRepo.Id;
                        model.Brands = _modelRepository.GetComboBrands(modelRepo.Id);
                        model.Models = _modelRepository.GetComboModels();
                        model.BrandIdUser = user.Id;
                    }
                }
            }

            model.Brands = _modelRepository.GetComboBrands(model.ModelId);
            model.Models = _modelRepository.GetComboModels();

            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Create(CarsViewModel modelView)
        {

            if (ModelState.IsValid)
            {
                var brand = await _modelRepository.GetBrandAsync(modelView.BrandId);
                var modelRepo = await _modelRepository.GetModelAsync(brand);

                modelView.Model = modelRepo.Name;
                modelView.Brand = brand.Name;


                var car = _converterHelper.ToCar(modelView, true);

                car.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await _carRepository.CreateAsync(car);
                return RedirectToAction(nameof(Index));
            }
            return View(modelView);
        }



        //// GET: Products/Edit/5


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("CarNotFound");
            }

            var car = await _carRepository.GetByIdAsync(id.Value);


            if (car == null)
            {
                return new NotFoundViewResult("CarNotFound");
            }
            foreach (var item in _carRepository.GetAll().Where(p => p.User.Email == User.Identity.Name))
            {
                if (id == item.Id)
                {
                    var model = _converterHelper.ToCarsViewModel(car);


                    model.Quantity = 1;
                    model.Cars = _carRepository.GetComboCars();


                    var brand = await _modelRepository.GetBrandAsync(car.BrandId);
                    if (brand != null)
                    {
                        var modelRepo = await _modelRepository.GetModelAsync(brand);
                        if (modelRepo != null)
                        {
                            model.ModelId = modelRepo.Id;
                            model.Brands = _modelRepository.GetComboBrands(modelRepo.Id);
                            model.Models = _modelRepository.GetComboModels();

                            return View(model);
                        }
                    }
                }
            }


            return new NotFoundViewResult("CarNotFound");
        }


        //// POST: Products/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Edit(CarsViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var brand = await _modelRepository.GetBrandAsync(model.BrandId);
                    var modelRepo = await _modelRepository.GetModelAsync(brand);

                    model.Model = modelRepo.Name;
                    model.Brand = brand.Name;


                    var car = _converterHelper.ToCar(model, false);


                    //TODO: Modificar para o user que tiver logado
                    car.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await _carRepository.UpdateAsync(car);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _carRepository.ExistAsync(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        //// GET: Products/Delete/5

        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("CarNotFound");
            }


            var car = await _carRepository.GetByIdAsync(id.Value);
            if (car == null)
            {
                return new NotFoundViewResult("CarNotFound");
            }

            foreach (var item in _carRepository.GetAll().Where(p => p.User.Email == User.Identity.Name))
            {
                if (id == item.Id)
                {
                    var model = _converterHelper.ToCarsViewModel(car);
                    return View(model);
                }
            }
            return View(car);
        }

        //// POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _carRepository.GetByIdAsync(id);

            try
            {
                //throw new Exception("Excepção de Teste");
                await _carRepository.DeleteAsync(product);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{product.Model} provavelmente está a ser usado!!";
                    ViewBag.ErrorMessage = $"{product.Model} não pode ser apagado visto haverem encomendas que o usam.</br></br>" +
                        $"Experimente primeiro apagar todas as encomendas que o estão a usar," +
                        $"e torne novamente a apagá-lo";
                }

                return View("Error");
            }

        }

        public IActionResult CarNotFound()
        {
            return View();
        }

    }
}
