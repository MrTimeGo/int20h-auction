using Auction.WebApi.Dto.Tag;

namespace Auction.WebApi.Services.Interfaces
{
    public interface ITagService
    {
        Task<List<TagDto>> GetAllTagsAsync();
    }
}
