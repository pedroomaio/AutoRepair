using AutoRepair.Data.Entities;
using AutoRepair.Helpers;
using AutoRepair.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data
{
    public class InspecionRepository : GenericRepository<Inspecion>, IInspecionRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        public InspecionRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public IQueryable GetAllWithCars(int id)
        {
            var linqInspecion = from ID in _context.InspecionDetails
                              join C in _context.Cars on ID.Car.Id equals C.Id
                              where ID.Car.Id == id
                              select new
                              {
                                  ID.Id,
                                  ID.Car,
                                  ID.EntryDate,
                                  ID.ExitDate,
                                  ID.InspesioStatus,
                                  ID.TotalPrice
                              };
            var car = _context.Cars;
            var details = _context.InspecionDetails;

            return linqInspecion;
        }
        public async Task AddItemToOrderAsync(AddItemViewModel model, string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return;
            }

            var product = await _context.Cars.FindAsync(model.CarId);
            if (product == null)
            {
                return;
            }

            var orderDetailTemp = await _context.InspecionDetailTemps
                .Where(odt => odt.User == user && odt.Car == product)
                .FirstOrDefaultAsync();

            if (orderDetailTemp == null)
            {
                orderDetailTemp = new InspecionDetailTemp
                {
                    Price = model.Price,
                    Car = product,
                    User = user,
                };

                _context.InspecionDetailTemps.Add(orderDetailTemp);
            }
            else
            {
                //orderDetailTemp.Quantity += model.Quantity;
                _context.InspecionDetailTemps.Update(orderDetailTemp);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ConfirmOrderAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return false;
            }

            var orderTmps = await _context.InspecionDetailTemps
                .Include(o => o.Car)
                .Where(o => o.User == user)
                .ToListAsync();

            if (orderTmps == null || orderTmps.Count == 0)
            {
                return false;
            }

            var details = orderTmps.Select(o => new InspecionDetails
            {
                TotalPrice = o.Price,
                Car = o.Car,
            }).ToList();

            decimal price = 0;
            foreach (var item in details)
            {
                price = item.TotalPrice;
            }
            var order = new Inspecion
            {
                InspecionDateStart = DateTime.UtcNow,
                User = user,
                Items = details,
                Price=price
            };

            await CreateAsync(order);
            _context.InspecionDetailTemps.RemoveRange(orderTmps);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteDetailTempAsync(int id)
        {
            var orderDetailTemp = await _context.InspecionDetailTemps.FindAsync(id);
            if (orderDetailTemp == null)
            {
                return;
            }

            _context.InspecionDetailTemps.Remove(orderDetailTemp);
            await _context.SaveChangesAsync();
        }

        public async Task DeliverOrder(MarkViewModel model)
        {
            var order = await _context.Inspecions.FindAsync(model.Id);
            if (order == null)
            {
                return;
            }

            order.InspecionDate = model.DeliveryDate;
            order.InspecionHours = model.InspecionHours;
            _context.Inspecions.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<InspecionDetailTemp>> GetDetailTempsAsync(string userName)
        {

            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            return _context.InspecionDetailTemps
                .Include(p => p.Car)
                .Where(o => o.User == user)
                .OrderBy(o => o.Car.RegisterCar);
        }

        public async Task<IQueryable<Inspecion>> GetOrderAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                return _context.Inspecions
                    .Include(o => o.User)
                    .Include(o => o.Items)
                    .ThenInclude(p => p.Car)
                    .OrderByDescending(o => o.InspecionDateStart);
            }

            return _context.Inspecions
                .Include(o => o.Items)
                .ThenInclude(p => p.Car)
                .Where(o => o.User == user)
                .OrderByDescending(o => o.InspecionDateStart);
        }

        public async Task<Inspecion> GetOrderAsync(int id)
        {
            return await _context.Inspecions.FindAsync(id);
        }

        public async Task ModifyOrderDetailTempQuantityAsync(int id, double quantity)
        {
            var orderDetailTemp = await _context.InspecionDetailTemps.FindAsync(id);
            if (orderDetailTemp == null)
            {
                return;
            }

            //orderDetailTemp.Quantity += quantity;
            //if (orderDetailTemp.Quantity > 0)
            //{
            //    _context.OrderDetailsTemp.Update(orderDetailTemp);
            //    await _context.SaveChangesAsync();
            //}
        }

    }
}
