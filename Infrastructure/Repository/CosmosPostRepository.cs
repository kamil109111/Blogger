using System;
using Domain.Entities.Cosmos;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cosmonaut;
using Cosmonaut.Extensions;
using System.Linq;

namespace Infrastructure.Repository
{
    public class CosmosPostRepository : ICosmosPostRepository
    {
        private readonly ICosmosStore<CosmosPost> _cosmosStore;

        public CosmosPostRepository(ICosmosStore<CosmosPost> cosmosStore)
        {
            _cosmosStore = cosmosStore;
        }

        public async Task<CosmosPost> AddAsync(CosmosPost post)
        {
            post.Id = Guid.NewGuid().ToString();
            return await _cosmosStore.AddAsync(post);
        }

        public async Task DeleteAsync(CosmosPost post)
        {
            await _cosmosStore.RemoveAsync(post);
        }

        public async Task<IEnumerable<CosmosPost>> GetAllAsync()
        {
            var posts = await _cosmosStore.Query().ToListAsync();
            return posts;
        }

        public async Task<CosmosPost> GetByIdAsync(string id)
        {
            return await _cosmosStore.FindAsync(id);
        }

        public async Task<IEnumerable<CosmosPost>> SearchByTitleAsync(string title)
        {
            var posts = await _cosmosStore.Query()
                .Where(p => p.Title.ToLower().Contains(title.ToLower()))
                .ToListAsync();

            return posts;
        }

        public async Task UpdateAsync(CosmosPost post)
        {
            await _cosmosStore.UpdateAsync(post);
        }
    }
}
