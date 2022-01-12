using System;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;

namespace Infrastructure.Repository
{
    public class DapperPostRepository : IDapperPostRepository
    {
        private readonly IDbConnection db;

        public DapperPostRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DapperBloggerCS"));
        }

        public IEnumerable<Post> GetAll()
        {
            var sql = "SELECT * FROM Posts";
            return db.Query<Post>(sql).ToList();
        }

        public Post GetById(int id)
        {
            var sql = "SELECT * FROM Posts WHERE Id = @Id";
            return db.Query<Post>(sql, new {@Id = id}).Single();
        }

        public Post Add(Post post)
        {
            var sql = "INSERT INTO Posts (Title, Content) VALUES (@Title, @Content);"
            + "SELECT CAST(SCOPE_IDENTITY() as int);";

            var id = db.Query<int>(sql, new
            {
                post.Title,
                post.Content,
                
            }).Single();

            post.Id = id;
            return post;
        }

        public void Update(Post post)
        {
            var sql =
                "UPDATE Posts SET Title = @Title, Content = @Content  WHERE Id = @Id";
            db.Execute(sql, post);
        }

        public void Delete(Post post)
        {
            var sql = "DELETE FROM Posts WHERE Id = @Id";
            db.Execute(sql, new {post.Id});
        }
    }
}
