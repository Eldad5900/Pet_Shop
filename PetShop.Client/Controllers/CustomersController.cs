using Microsoft.AspNetCore.Mvc;
using PetShop.Data.Model;
using PetShop.Service;

namespace PetShop.Client.Controllers
{
    public class CustomersController : Controller
    {
        private readonly UserService _myService;
        public CustomersController(UserService context)
        {
            _myService = context;
        }
        public async Task<IActionResult> Index(int id)
        {
            var animal = await _myService.GetAnimal(id);
            if (animal == null) return NotFound();
            return View(animal);
        }

        public async Task<IActionResult> AddComment(Animal animal, string myComment)
        {
            if (ModelState.IsValid)
            {
                var newComment = new Comment { AnimelId = animal.AnimalId, Content = myComment };
                await _myService.AddCommant(newComment);
            }
            return RedirectToAction("Index", new { id = animal.AnimalId });
        }
    }

}
