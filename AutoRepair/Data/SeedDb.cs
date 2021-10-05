using AutoRepair.Data.Entities;
using AutoRepair.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoRepair.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");
            await _userHelper.CheckRoleAsync("Employee");


            var user = await _userHelper.GetUserByEmailAsync("repairnauto@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Auto",
                    LastName = "Repair",
                    Email = "repairnauto@gmail.com",
                    UserName = "repairnauto@gmail.com",
                    PhoneNumber = "123456789",
                    Address = "Rua Salmão Real 123 nº6",
                    AgreeTerm=true
                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

        }
      
    }
}
