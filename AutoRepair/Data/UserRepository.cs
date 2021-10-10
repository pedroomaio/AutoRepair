using AutoRepair.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public List<User> GetAllUser()
        {
            var list = _context.Users.Select(p => new SelectListItem
            {
                Text = p.UserName,
                Value = p.Id.ToString()
            }).ToList();


            var model = new List<User>();

            foreach (var item in _context.Users)
            {
                model.Add(new User
                {
                    FirstName = item.FullName,
                    Email = item.Email,
                    EmailConfirmed = item.EmailConfirmed

                });
            }

            return model;

        }
        public IQueryable<User> GetAll()
        {
            return _context.Users.AsNoTracking();
        }
        public async Task<User> GetByIdAsync(string id)
        {

            var a = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ConcurrencyStamp == id);
            return a;
        }


        public async Task CreateAsync(User entity)
        {
            await _context.Set<User>().AddAsync(entity);
            await SaveAllAsync();
        }


        public async Task UpdateAsync(User entity)
        {
            _context.Set<User>().Update(entity);
            await SaveAllAsync();
        }


        public async Task DeleteAsync(User entity)
        {
            _context.Set<User>().Remove(entity);
            await SaveAllAsync();
        }


        public async Task<bool> ExistAsync(string id)
        {
            return await _context.Set<User>().AnyAsync(e => e.Id == id);
        }


        private async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
