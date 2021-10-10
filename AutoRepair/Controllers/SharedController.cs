using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoRepair.Data;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepair.Controllers
{
    public class SharedController : Controller
    {
        private readonly IUserRepository _userRepository;

        public SharedController(
            IUserRepository userRepository)

        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
   var a = _userRepository.GetAll();
            return View();
        }
        public IActionResult UserImage()
        {
            var a = _userRepository.GetAll();
            return View();
        }
    }
}
