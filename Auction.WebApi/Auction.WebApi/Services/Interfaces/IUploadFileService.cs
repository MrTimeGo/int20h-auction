namespace Auction.WebApi.Services.Interfaces
{
    public interface IUploadFileService
    {
        public Task<string[]> UploadListFilesAsync(List<IFormFile> files);
        public Task<string> UploadFileAsync(IFormFile file);
    }
}
