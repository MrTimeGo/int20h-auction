using Auction.WebApi.Dto.File;
using Auction.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Auction.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaticFileController(IFileService fileService) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<List<FileDto>>> UploadFile(List<IFormFile> files)
        {
            return await fileService.CreateFilesAsync(files);
        }
    }
}
