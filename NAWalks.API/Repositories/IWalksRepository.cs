using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalksRepository
    {

        public Task<Walk> CreateAsync(Walk walk);
        public Task<List<Walk>> GetAllAsync( string? filterOn =null,  string? filterQuery=null,
            string? sortBy=null,  bool isAscending=true,
            int pageNumber =1 , int pageSize =10);
        public Task<Walk?> GetByIdAsync(Guid id);
        public Task<Walk?> UpdateAsync(Guid id, Walk walk);
        public Task<Walk?> DeleteAsync(Guid id);



    }
}
