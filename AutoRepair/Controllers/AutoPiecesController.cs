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
    public class AutoPiecesController : Controller
    {
        private readonly IAutoPieceRepository _autoPieceRepository;
        //private readonly IUserHelper _userHelper;
        ////private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;

        public AutoPiecesController(
            IAutoPieceRepository autoPieceRepository,
        //IUserHelper userHelper,
        ////IBlobHelper blobHelper)
        IConverterHelper converterHelper)
        {
            _autoPieceRepository = autoPieceRepository;
            //_userHelper = userHelper;
            ////_blobHelper = blobHelper;
            _converterHelper = converterHelper;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View(_autoPieceRepository.GetAll());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }

            var product = await _autoPieceRepository.GetByIdAsync(id.Value);
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
        public async Task<IActionResult> Create(AutoPiecesViewModel model)
        {
            if (ModelState.IsValid)
            {

                var product = _autoPieceRepository.ToAutoPiece(model, true);

                await _autoPieceRepository.CreateAsync(product);
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

            var product = await _autoPieceRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return new NotFoundViewResult("ProductNotFound");
            }


            var model = _autoPieceRepository.ToAutoPieceViewModel(product);
            return View(model);
        }


        ////// POST: Products/Edit/5
        ////// To protect from overposting attacks, enable the specific properties you want to bind to.
        ////// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AutoPiecesViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    var product = _autoPieceRepository.ToAutoPiece(model, false);


                    await _autoPieceRepository.UpdateAsync(product);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _autoPieceRepository.ExistAsync(model.Id))
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

            var car = await _autoPieceRepository.GetByIdAsync(id.Value);
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
            var product = await _autoPieceRepository.GetByIdAsync(id);

            try
            {
                //throw new Exception("Excepção de Teste");
                await _autoPieceRepository.DeleteAsync(product);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{product.AutoPieceName} provavelmente está a ser usado!!";
                    ViewBag.ErrorMessage = $"{product.AutoPieceName} não pode ser apagado visto haverem encomendas que o usam.</br></br>" +
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

