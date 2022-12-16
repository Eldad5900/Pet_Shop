using Microsoft.AspNetCore.Mvc;
using PetShop.Client.Models;
using PetShop.Service;
using System.Diagnostics;

namespace PetShop.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserService _userService;

        public HomeController(ILogger<HomeController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
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
        public async Task<IActionResult> Starting()
        {
            return View(await _userService.GetTop());
        }
        public async Task<IActionResult> Customers()
        {
            return View(_userService.GetAllAnimal());
        }
    }

}