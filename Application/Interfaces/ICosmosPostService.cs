using Application.Dto.Cosmos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICosmosPostService
    {
        Task<IEnumerable<CosmosPostDto>> GetAllPostsAsync();
        Task<CosmosPostDto> GetPostByIdAsync(string id);
        Task<CosmosPostDto> AddNewPostAsync(CreateCosmosPostDto newPost);
        Task UpdatePostAsync(UpdateCosmosPostDto updatePostDto);
        Task DeletePostAsync(string id);
        Task<IEnumerable<CosmosPostDto>> SearchPostByTitleAsync(string title);
    }
}
