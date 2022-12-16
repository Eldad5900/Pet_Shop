using PetShop.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Service.Interfes
{
    public interface IServices<T> where T : class
    {
        Task<T> GetAnimal(int id);
        IQueryable<T> GetAllAnimal();
        Task<T> AddAnimal(T entity);
        Task<T> DeleteAnimal(int id);
    }
    public interface IAnimalServics : IServices<Animal> { }
}
