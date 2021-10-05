using AutoRepair.Data;
using AutoRepair.Data.Entities;
using AutoRepair.Helpers;
using AutoRepair.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vereyon.Web;

namespace AutoRepair.Controllers
{
    public class MechanicsController : Controller
    {
            private readonly IMechanicRepository _mechanicRepository;
            private readonly IFlashMessage _flashMessage;

            public MechanicsController(
                IMechanicRepository mechanicRepository,
            IFlashMessage flashMessage)
            {
                _mechanicRepository = mechanicRepository;
                _flashMessage = flashMessage;
            }

            public async Task<IActionResult> DeleteMechanic(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var city = await _mechanicRepository.GetMechanicAsync(id.Value);
                if (city == null)
                {
                    return NotFound();
                }

                var countryId = await _mechanicRepository.DeleteMechanicAsync(city);
                return this.RedirectToAction($"Details", new { id = countryId });
            }

            public async Task<IActionResult> EditMechanic(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var city = await _mechanicRepository.GetMechanicAsync(id.Value);
                if (city == null)
                {
                    return NotFound();
                }

                return View(city);
            }


            [HttpPost]
            public async Task<IActionResult> EditMechanic(Mechanic city)
            {
                if (this.ModelState.IsValid)
                {
                    var countryId = await _mechanicRepository.UpdateMechanicAsync(city);
                    if (countryId != 0)
                    {
                        return this.RedirectToAction($"Details", new { id = countryId });
                    }
                }

                return this.View(city);
            }

            public async Task<IActionResult> AddMechanic(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var specialistType = await _mechanicRepository.GetByIdAsync(id.Value);
                if (specialistType == null)
                {
                    return NotFound();
                }

                var model = new MechanicViewModel { SpecialistTypeId = specialistType.Id };
                return View(model);
            }

            [HttpPost]
            public async Task<IActionResult> AddMechanic(MechanicViewModel model)
            {
                if (this.ModelState.IsValid)
                {
                    await _mechanicRepository.AddMechanicAsync(model);
                    return RedirectToAction("Details", new { id = model.SpecialistTypeId });
                }

                return this.View(model);
            }

            public IActionResult Index()
            {
                return View(_mechanicRepository.GetSpecialistTypeWithMechanic());
            }

            public async Task<IActionResult> Details(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var country = await _mechanicRepository.GetSpecialistTypeWithMechanicAsync(id.Value);
                if (country == null)
                {
                    return NotFound();
                }

                return View(country);
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(SpecialistType specialistType)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        await _mechanicRepository.CreateAsync(specialistType);
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception)
                    {
                        _flashMessage.Danger("This country already exist!");
                    }

                    return View(specialistType);
                }

                return View(specialistType);
            }

            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var country = await _mechanicRepository.GetByIdAsync(id.Value);
                if (country == null)
                {
                    return NotFound();
                }
                return View(country);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(SpecialistType specialistType)
            {
                if (ModelState.IsValid)
                {
                    await _mechanicRepository.UpdateAsync(specialistType);
                    return RedirectToAction(nameof(Index));
                }

                return View(specialistType);
            }

            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var country = await _mechanicRepository.GetByIdAsync(id.Value);
                if (country == null)
                {
                    return NotFound();
                }

                await _mechanicRepository.DeleteAsync(country);
                return RedirectToAction(nameof(Index));
            }

        }
    
}
