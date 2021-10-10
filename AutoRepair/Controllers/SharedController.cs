using System.Linq;
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
        public IActionResult _ViewShared()
        {
            return View(_userRepository.GetAll().Where(p => p.Email == User.Identity.Name));

        }
    }
}
