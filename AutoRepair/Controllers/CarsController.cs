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

        public CarsController(
            ICarRepository carRepository,
            IUserHelper userHelper,
            //IBlobHelper blobHelper)
            IConverterHelper converterHelper)
        {
            _carRepository = carRepository;
            _userHelper = userHelper;
            //_blobHelper = blobHelper;
            _converterHelper = converterHelper;
        }

        // GET: Products
        public IActionResult Index()
        {
            //var user = await _userHelper.GetUserByEmailAsync(User.Identity.Name);
            //var a = _userHelper.CheckRoleAsync("Admin");
            //var b = _userHelper.IsUserInRoleAsync(user, "Admin");
            ////if ()
            ////{
            ////    return View(_carRepository.GetAll().Where(p => p.User.Email == User.Identity.Name).OrderBy(p => p.Modelo));
            ////}
            ////else
            ////{
            ////    return View(_carRepository.GetAll().OrderBy(p => p.Modelo));
            ////}
            return View(_carRepository.GetAll().Where(p => p.User.Email == User.Identity.Name).OrderBy(p => p.Modelo));
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var product = await _carRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            return View(product);
        }


        //// GET: Products/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarsViewModel model)
        {
            if (ModelState.IsValid)
            {

                var product = _converterHelper.ToProduct(model, true);

                product.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await _carRepository.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }



        //// GET: Products/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var product = await _carRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }


            var model = _converterHelper.ToProductViewModel(product);
            return View(model);
        }


        //// POST: Products/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CarsViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    //Guid imageId = model.ImageId;

                    //if (model.ImageFile != null && model.ImageFile.Length > 0)
                    //{
                    //    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "products");
                    //}

                    var product = _converterHelper.ToProduct(model, false);


                    //TODO: Modificar para o user que tiver logado
                    product.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await _carRepository.UpdateAsync(product);

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
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var car = await _carRepository.GetByIdAsync(id.Value);
            if (car == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            return View(car);
        }

        //// POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
                    ViewBag.ErrorTitle = $"{product.Modelo} provavelmente está a ser usado!!";
                    ViewBag.ErrorMessage = $"{product.Modelo} não pode ser apagado visto haverem encomendas que o usam.</br></br>" +
                        $"Experimente primeiro apagar todas as encomendas que o estão a usar," +
                        $"e torne novamente a apagá-lo";
                }

                return View("Error");
            }

        }

        public IActionResult ProductNotFound()
        {
            return View();
        }

    }
}
