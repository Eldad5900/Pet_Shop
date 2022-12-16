using Microsoft.EntityFrameworkCore;
using PetShop.Data.Contexts;
using PetShop.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Repository
{
    public class AnimalRepository : IAnimalReposetory
    {
        private readonly PetShopDataContext _context;
        public AnimalRepository(PetShopDataContext context)
        {
            _context = context;
        }
        public async Task<Animal> Delete(int id)
        {
            var animal = await Get(id);
            if (animal == null) return null;
            var list = _context.Comments.ToList();
            foreach (var item in list)
            {
                if(item.AnimelId == id)
                {
                    _context.Comments.Remove(item);
                }
                
            }

            _context.Animals.Remove( animal);
           
            await _context.SaveChangesAsync();
            return animal;
        }

        public async Task<Animal> Get(int id)
        {
            var animal = await _context.Animals.FirstOrDefaultAsync(animal => animal.AnimalId == id);
            if (animal == null) return null;
            return animal;
        }

        public IQueryable<Animal>GetAll()
        {
            return _context.Animals.Include(a => a.Category)
                .Include(a => a.Comments);
        }
        public IQueryable<Animal> GetAllById(string categoryName)
        {
            var animals = _context.Animals.Include(a => a.Category)
                .Include(a => a.Comments).Where(animal => animal.Category.Name == categoryName);
            return animals;
        }

        public async Task<Animal> GetT(string name)
        {
           var animal = await _context.Animals.FirstOrDefaultAsync(animal => animal.Name == name);
            if (animal == null) return null;
            return animal;
        }

        

        public async Task<Animal> Add(Animal entity)
        {
           await _context.Animals.AddAsync(entity);
            _context.SaveChanges();
            return entity;
        }

        public IQueryable<Categry> GetAllCatgories() =>  _context.Categries;

        public async Task<Animal> Updete(Animal animal)
        {
            _context.Update(animal);
            await _context.SaveChangesAsync();
            return animal;
        }
    }
}
