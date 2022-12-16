using Microsoft.VisualStudio.TestTools.UnitTesting;
using PetShop.Data.Contexts;
using PetShop.Data.Model;
using PetShop.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShop.UnitTests
{
    
    [TestClass]
    public class UnitTest1
    {
         
        private AnimalRepository _animalRepository { get; set; }
        private CommentRepository _commentRepository { get; set; }
        Categry Categry = new Categry();
        [TestMethod]
        public async void AddComment()
        {
            await _commentRepository.Add(new Comment { AnimelId = 1 , CommentId =1 , Content="DSDSDS" });
            Assert.IsTrue(_commentRepository.Equals(8));
        }
        [TestMethod]
        public async void AddAnimale()
        {
            await _animalRepository.Add(new Animal { AnimalId = 1, BirthDate = DateTime.Now, Category = Categry, CategoryId = 1, Description = "assd", Name = "dsds", PhtotoUrl = "sdssd" }) ;
            Assert.IsTrue(_animalRepository.Equals(8));
        }
        [TestMethod]
        public async void DeliteComment()
        {
            await _commentRepository.commentDelete(1);
            Assert.IsTrue(_commentRepository.Equals(7));
        }
        [TestMethod]
        public async void DeliteAnimal()
        {
            await _animalRepository.Delete(1);
            Assert.IsTrue(_animalRepository.Equals(7));
        }
        [TestMethod]
        public async void GetCommentById()
        {
           var comments = await _commentRepository.GetByAnimalId(1);
            Assert.IsNotNull(comments);
        }
        [TestMethod]
        public void GetAllComment()
        {
         
            var comment = _commentRepository.GetAll();
            Assert.IsNotNull(comment);
        }
        [TestMethod]
        public async void GetAllAnimal()
        {
            await _animalRepository.Add(new Animal { AnimalId = 1, BirthDate = DateTime.Now, Category = Categry, CategoryId = 1, Description = "assd", Name = "dsds", PhtotoUrl = "sdssd" });
            var animal =(IQueryable<Comment>)_animalRepository.GetAll();
            Assert.IsNotNull(animal);
        }
        [TestMethod]
        public void GetAllCatgory()
        {
            var category = _animalRepository.GetAllCatgories();
            Assert.IsNotNull(category);

            
        }
        [TestMethod]
        public async void GetAnimalById()
        {
                var animal = await _animalRepository.GetT("dog");
            Assert.IsInstanceOfType(animal, typeof(Animal));
        }
        [TestMethod]
        public async void Updete()
        {
           var animal  =  await _animalRepository.Updete(new Animal { AnimalId = 1, BirthDate = DateTime.Now, Category = Categry, CategoryId = 1, Description = "assd", Name = "dsds", PhtotoUrl = "sdssd" });
           Assert.IsNotNull(animal);
        }


    }
}