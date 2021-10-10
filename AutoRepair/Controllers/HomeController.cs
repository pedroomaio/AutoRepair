using AutoRepair.Data;
using AutoRepair.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AutoRepair.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly IUserRepository _userRepository;

        public HomeController(
            IUserRepository userRepository)

        {
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            var a = _userRepository.GetAll();
            return View(a);
        }
        public IActionResult Jornal(int a1, int a2)
        {
            int a = a1 + a2;
            return View(a);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
