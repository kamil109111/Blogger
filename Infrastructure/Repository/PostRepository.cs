using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.ExtensionMethods;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Infrastructure.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly BloggerContext _context;

        public PostRepository(BloggerContext context)
        {
            _context = context;
        }

        public async Task<Post> AddAsync(Post post)
        {         
           var createdPost = await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return createdPost.Entity;
        }

        public async Task DeleteAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Post>> GetAllAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterBy)
        {
           return await _context.Posts
               .Where(m => m.Title.ToLower()
                   .Contains(filterBy.ToLower()) || m.Content.ToLower().Contains(filterBy.ToLower()))
               .OrderByPropertyName(sortField, ascending)
               .Skip((pageNumber -1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
        }

        public async Task<int> GetAllCountAsync(string filterBy)
        {
            return await _context.Posts.Where(m => m.Title.ToLower()
                .Contains(filterBy.ToLower()) || m.Content.ToLower().Contains(filterBy.ToLower()))
                .CountAsync();
        }

        public async Task<Post> GetByIdAsync(int id)
        {
            return await _context.Posts.SingleOrDefaultAsync(p => p.Id == id);    
        }

        public async Task<IEnumerable<Post>> SearchByTitleAsync(string title)
        {

            var posts = await _context.Posts
               .Where(p => p.Title.Contains(title))
               .ToListAsync();
            
            return  posts;
        }

        public async Task UpdateAsync(Post post)
        {            
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}
