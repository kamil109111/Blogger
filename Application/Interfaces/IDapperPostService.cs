using Application.Dto.Dapper;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IDapperPostService
    {
        IEnumerable<DapperPostDto> GetAllPosts();
        DapperPostDto GetById(int id);
        DapperPostDto Add(DapperCreatePostDto newPost);
        void Update(DapperUpdatePostDto updatePost);
        void Delete(int id);
    }
}
