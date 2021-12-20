using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly BloggerContext _context;

        public PostRepository(BloggerContext context)
        {
            _context = context;
        }

        public Post Add(Post post)
        {         
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post;
        }

        public void Delete(Post post)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }

        public IEnumerable<Post> GetAll()
        {
           return _context.Posts;
        }

        public Post GetById(int id)
        {
            return _context.Posts.SingleOrDefault(p => p.Id == id);    
        }

        public IEnumerable<Post> SearchByTitle(string title)
        {
            ISet<Post> posts = new HashSet<Post>();

            foreach (var post in _context.Posts)
            {
                bool contain = post.Title.ToLower().Contains(title.ToLower());
                if (contain == true)
                {
                    posts.Add(post);
                }
            }
            return posts;
        }

        public void Update(Post post)
        {            
            _context.Posts.Update(post);
            _context.SaveChanges();
        }
    }
}
