using AutoRepair.Data;
using AutoRepair.Helpers;
using AutoRepair.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AutoRepair.Controllers
{
    public class SpecialistTypeController : Controller
    {
        private readonly ISpecialistTypeRepository _specialistTypeRepository;

        public SpecialistTypeController(
            ISpecialistTypeRepository specialistTypeRepository)
        {
            _specialistTypeRepository = specialistTypeRepository;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View(_specialistTypeRepository.GetAll());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("SpecialistTypeNotFound");
            }

            var product = await _specialistTypeRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return new NotFoundViewResult("SpecialistTypeNotFound");
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
        public async Task<IActionResult> Create(SpecialistTypeViewModel model)
        {
            if (ModelState.IsValid)
            {

                var product = _specialistTypeRepository.ToSpecialistType(model, true);

                await _specialistTypeRepository.CreateAsync(product);
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
                return new NotFoundViewResult("SpecialistTypeNotFound");
            }

            var product = await _specialistTypeRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return new NotFoundViewResult("SpecialistTypeNotFound");
            }


            var model = _specialistTypeRepository.ToSpecialistTypeViewModel(product);
            return View(model);
        }


        ////// POST: Products/Edit/5
        ////// To protect from overposting attacks, enable the specific properties you want to bind to.
        ////// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialistTypeViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    var product = _specialistTypeRepository.ToSpecialistType(model, false);


                    await _specialistTypeRepository.UpdateAsync(product);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _specialistTypeRepository.ExistAsync(model.Id))
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
                return new NotFoundViewResult("SpecialistTypeNotFound");
            }

            var car = await _specialistTypeRepository.GetByIdAsync(id.Value);
            if (car == null)
            {
                return new NotFoundViewResult("SpecialistTypeNotFound");
            }

            return View(car);
        }

        ////// POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _specialistTypeRepository.GetByIdAsync(id);

            try
            {
                //throw new Exception("Excepção de Teste");
                await _specialistTypeRepository.DeleteAsync(product);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{product.SpecialistTypeName} provavelmente está a ser usado!!";
                    ViewBag.ErrorMessage = $"{product.SpecialistTypeName} não pode ser apagado visto haverem encomendas que o usam.</br></br>" +
                        $"Experimente primeiro apagar todas as encomendas que o estão a usar," +
                        $"e torne novamente a apagá-lo";
                }

                return View("Error");
            }

        }

        public IActionResult SpecialistTypeNotFound()
        {
            return View();
        }

    }
}
