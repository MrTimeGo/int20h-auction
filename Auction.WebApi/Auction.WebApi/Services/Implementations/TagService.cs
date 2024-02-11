using Auction.WebApi.Data;
using Auction.WebApi.Dto.Tag;
using Auction.WebApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Auction.WebApi.Services.Implementations
{
    public class TagService(AuctionContext context) : ITagService
    {
        public async Task<List<TagDto>> GetAllTagsAsync()
        {
            return await context.Tags.Select(t => new TagDto
            {
                Name = t.Name,
            })
                .ToListAsync();
        }
    }
}
