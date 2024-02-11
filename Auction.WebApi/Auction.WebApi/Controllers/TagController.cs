using Auction.WebApi.Dto.Tag;
using Auction.WebApi.Services.Implementations;
using Auction.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auction.WebApi.Controllers
{
    [Route("api/tags")]
    [ApiController]
    [Authorize]
    public class TagController(ITagService tagService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<TagDto>>> GetAllTags()
        {
            return await tagService.GetAllTagsAsync();
        }
    }
}
