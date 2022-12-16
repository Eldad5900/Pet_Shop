using PetShop.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShop.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace PetShop.Data.Repository
{
    public class CommentRepository : ICommentReposiTory
    {
        private readonly PetShopDataContext _context;
        public CommentRepository(PetShopDataContext petData)
        {
             _context=petData ;
        }

        public async Task< Comment> Add(Comment entity)
        {
            
            if (entity == null) return null;
            await _context.Comments.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Comment> commentDelete(int id)
        {
            var comment = await Get(id);
            if (comment == null) return null;
           _context.Comments.Remove(comment);
           await _context.SaveChangesAsync();
            return  comment;
        }

        public async Task<Comment> Get(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(comments => comments.CommentId == id);
            if (comment == null) return null;
            return comment;
        }

        public IQueryable<Comment> GetAll()
        {
           return _context.Comments;
        }

        public async Task< IAsyncEnumerable<Comment>> GetByAnimalId(int animalId)
        {
            var comment =  _context.Comments.Where(comments => comments.AnimelId == animalId);
            if (comment == null) return null;
            return (IAsyncEnumerable<Comment>) comment;
        }
    }
}
