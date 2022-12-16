using PetShop.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Data.Repository
{
    public  interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        IQueryable<T> GetAll();
        
    }
    public interface IAnimalReposetory : IRepository<Animal>
    {
        Task< Animal >GetT(string name);
        Task<Animal>Add(Animal animal);
        Task<Animal> Updete(Animal animal);
        IQueryable<Categry> GetAllCatgories();
        Task<Animal> Delete(int id);
        IQueryable<Animal>GetAllById(string category);
    }
    public interface ICommentReposiTory : IRepository<Comment>
    { 
        Task<Comment> commentDelete(int id);
        Task<IAsyncEnumerable<Comment>> GetByAnimalId(int animalId);
        Task<Comment> Add(Comment entity);
    }
    public interface ICatgoryRepository : IRepository<Categry>  
    {
        IEnumerable<Categry> GetByAnimalId(int animalId);
        Categry GetT(string name);
    }



}
