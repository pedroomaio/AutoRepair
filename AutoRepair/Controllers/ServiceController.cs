using AutoRepair.Data;
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
    public class ServiceController : Controller
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceController(
            IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View(_serviceRepository.GetAll());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var product = await _serviceRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            return View(product);
        }


        ////// GET: Products/Create
        //[Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServicesViewModel model)
        {
            if (ModelState.IsValid)
            {

                var product = _serviceRepository.ToService(model, true);

                await _serviceRepository.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }



        ////// GET: Products/Edit/5
        //[Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var product = await _serviceRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }


            var model = _serviceRepository.ToServiceViewModel(product);
            return View(model);
        }


        ////// POST: Products/Edit/5
        ////// To protect from overposting attacks, enable the specific properties you want to bind to.
        ////// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServicesViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    var product = _serviceRepository.ToService(model, false);


                    await _serviceRepository.UpdateAsync(product);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _serviceRepository.ExistAsync(model.Id))
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

        ////// GET: Products/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("AutoPieceNotFound");
            }

            var car = await _serviceRepository.GetByIdAsync(id.Value);
            if (car == null)
            {
                return new NotFoundViewResult("AutoPieceNotFound");
            }

            return View(car);
        }

        ////// POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _serviceRepository.GetByIdAsync(id);

            try
            {
                //throw new Exception("Excepção de Teste");
                await _serviceRepository.DeleteAsync(product);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{product.ServiceName} provavelmente está a ser usado!!";
                    ViewBag.ErrorMessage = $"{product.ServiceName} não pode ser apagado visto haverem encomendas que o usam.</br></br>" +
                        $"Experimente primeiro apagar todas as encomendas que o estão a usar," +
                        $"e torne novamente a apagá-lo";
                }

                return View("Error");
            }

        }

        public IActionResult AutoPieceNotFound()
        {
            return View();
        }

    }
}
