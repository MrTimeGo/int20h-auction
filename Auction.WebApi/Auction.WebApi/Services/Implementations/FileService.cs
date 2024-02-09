using Auction.WebApi.Data;
using Auction.WebApi.Dto.File;
using Auction.WebApi.Entities;
using Auction.WebApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Auction.WebApi.Services.Implementations
{
    public class FileService(IUploadFileService uploadFileService, AuctionContext context, IMapper mapper) : IFileService
    {
        public async Task<List<FileDto>> CreateFilesAsync(List<IFormFile> files)
        {
            var urls = await uploadFileService.UploadListFilesAsync(files);
            var uploadedFiles = urls.Select(url => new StaticFile
            {
                FilePath = url,
                CreatedAt = DateTime.UtcNow
            });
            context.AddRange(uploadedFiles);
            await context.SaveChangesAsync();

            var result = await context.StaticFiles.Where(file => urls.Contains(file.FilePath))
             .ToListAsync();
            return mapper.Map<List<FileDto>>(result)!;
        }
    }
}
