using Auction.WebApi.Dto.File;

namespace Auction.WebApi.Services.Interfaces
{
    public interface IFileService
    {
        public Task<List<FileDto>> CreateFilesAsync(List<IFormFile> files);
    }
}
