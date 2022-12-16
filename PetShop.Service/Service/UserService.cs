using PetShop.Data.Model;
using PetShop.Data.Repository;
using PetShop.Service.Interfes;

namespace PetShop.Service
{
    public class UserService : IAnimalServics
    {
        private readonly IAnimalReposetory _animal;
        private readonly ICommentReposiTory _command;
     
        public UserService(IAnimalReposetory animals,ICommentReposiTory commentReposiTory)
        {
            _animal = animals;
            _command = commentReposiTory;
        }

        public async Task<IEnumerable<Animal>> GetTop()
        {
            var animals = _animal.GetAll();
            var twoAnimal = animals.OrderByDescending(a => a.Comments.Count).Take(2);
            return twoAnimal;
        }

        public async Task<Animal> AddAnimal(Animal animal)
        {
            await _animal.Add(animal);
            return animal;
        }
        public async Task<Animal> DeleteAnimal(int id)
        {
           return await _animal.Delete(id);
        }

        public async Task<Animal> GetAnimal(int id)
        {
            return await _animal.Get(id);
        }

        public IQueryable<Animal> GetAllAnimal()
        {
            return _animal.GetAll();
        }
        public  IQueryable<Animal> GetAllAnimalById(string categry)
        {
            return _animal.GetAllById(categry);
        }

        public async Task<Animal> Update(Animal animal)
        {
            await _animal.Updete(animal);
            return animal;
        }

        public IQueryable<Categry> GetCategries() => _animal.GetAllCatgories();

        public async Task<Comment> AddCommant(Comment entity)
        {
            await _command.Add(entity);
            return entity;
        }

        public async Task<Comment> DeleteCommant(int itemid)
        {
          return await _command.commentDelete(itemid);
        }

        public async Task<Comment> GetCommant(int id)
        {
            return await _command.Get(id);
        }

        public IQueryable<Comment> GetAllCommants()
        {
            return _command.GetAll();
        }

        public async Task<IAsyncEnumerable<Comment>> GetByAnimalId(int animalId)
        {
            return await _command.GetByAnimalId(animalId);
        }
    }
}