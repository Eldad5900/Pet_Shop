#nullable disable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.Data.Model;
using PetShop.Service;

namespace PetShop.Client.Controllers
{  
    public class AnimalsController : Controller
    {

        private readonly UserService _myService;


        public AnimalsController(UserService context)
        {
            _myService = context;
        }

        
        public async Task<IActionResult> Index(string? category)
        {
            if (category == null)
            {
                var animals = _myService.GetAllAnimal();
                return View(animals);
            }
            else
            {
               var animal = GetAnimals(category);
               return View(animal);
            }
            
        }
        public IActionResult GetAnimalsByCategoryID(string categoryName)
        {
            if(categoryName == "All")
            {
                return RedirectToAction("Index");
            }
           return RedirectToAction("Index", new { category = categoryName});

        }
        public  IQueryable GetAnimals(string categoryID)
        {
            var animal = _myService.GetAllAnimalById(categoryID);
            return animal;
        }

        public async Task<IActionResult> Details(int id)
        {
            var animal = await _myService.GetAnimal(id);
            if (animal == null) return NotFound();
            return View(animal);
        }


        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_myService.GetCategries(), "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimalId,Name,Description,BirthDate,PhtotoUrl,CategoryId")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                await _myService.AddAnimal(animal);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_myService.GetCategries(), "CategoryId", "Name", animal.CategoryId);
            return View(animal);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var animal = await _myService.GetAnimal(id);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_myService.GetCategries(), "CategoryId", "Name", animal.CategoryId);// GetAllCtegoris
            return View(animal);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimalId,Name,Description,BirthDate,PhtotoUrl,CategoryId")] Animal animal)
        {
            if (id != animal.AnimalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _myService.Update(animal);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.AnimalId))
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
            ViewData["CategoryId"] = new SelectList(_myService.GetCategries(), "CategoryId", "Name", animal.CategoryId);
            return View(animal);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _myService.GetAllAnimal()
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.AnimalId == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _myService.GetAnimal(id);
            if (animal != null)
            {
                await _myService.DeleteAnimal(animal.AnimalId);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _myService.GetAnimal(id) == null;
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(Animal animal, string myComment)
        {
            if (ModelState.IsValid)
            {
                var newComment = new Comment { AnimelId = animal.AnimalId, Content = myComment };
                await _myService.AddCommant(newComment);
            }
            return RedirectToAction("Details", new { id = animal.AnimalId });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteComment(int itemid)
        {
            var deletedComment = await _myService.DeleteCommant(itemid);
            return RedirectToAction("Details", new { id = deletedComment.AnimelId });
        }


    }
}
