using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class PostRepository : IPostRepository
    {
        private static readonly ISet<Post> _posts = new HashSet<Post>()
        {
            new Post(1, "Jak zostać programistą?", "Treść 1"),
            new Post(2, "Ile zarabia programista?", "Treść 2"),
            new Post(3, "Dlaczego warto zostać programistą?", "Treść 3")
        };
        public Post Add(Post post)
        {
            post.Id = _posts.Count() + 1;
            post.Created = DateTime.UtcNow;
            _posts.Add(post);
            return post;
        }

        public void Delete(Post post)
        {
            _posts.Remove(post);
        }

        public IEnumerable<Post> GetAll()
        {
           return _posts;
        }

        public Post GetById(int id)
        {
            return _posts.SingleOrDefault(p => p.Id == id);    
        }

        public IEnumerable<Post> SearchByTitle(string title)
        {
            ISet<Post> posts = new HashSet<Post>();

            foreach (var post in _posts)
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
            post.LastModified = DateTime.UtcNow;
        }
    }
}
