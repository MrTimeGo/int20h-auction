using Auction.WebApi.Dto;
using Auction.WebApi.Dto.Lot;
using Auction.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auction.WebApi.Controllers;
[Route("api/lots")]
[ApiController]
public class LotController(ILotService lotService) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<PaginationResult<LotDto>>> GetLotList(
            [FromQuery] string? searchTerm,
            [FromQuery] LotFilter? filters,
            [FromQuery] LotSort? sort,
            [FromQuery] PaginationModel? pagination
        )
    {
        return Ok(await lotService.GetLotsAsync(searchTerm, filters, sort, pagination));
    }
}
