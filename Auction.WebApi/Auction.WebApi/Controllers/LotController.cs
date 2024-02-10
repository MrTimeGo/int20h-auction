using Auction.WebApi.Dto;
using Auction.WebApi.Dto.Bet;
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
            [FromQuery] LotFilter filters,
            [FromQuery] LotSort sort,
            [FromQuery] PaginationModel pagination
        )
    {
        return Ok(await lotService.GetLotsAsync(searchTerm, filters, sort, pagination));
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<LotDto>> PostLot([FromBody] CreateLotDto dto)
    {
        return Ok(await lotService.CreateLotAsync(dto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LotDetailedDto>> GetLotById([FromRoute] Guid id)
    {
        return Ok(await lotService.GetLotByIdAsync(id));
    }

    [HttpPost("{id}/make-bet")]
    [Authorize]
    public async Task<IActionResult> MakeBet([FromBody] MakeBetDto dto, [FromRoute] Guid id)
    {
        await lotService.MakeBet(id, dto);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<LotDetailedDto>> UpdateLotById([FromRoute] Guid id, [FromBody] CreateLotDto dto)
    {
        return Ok(await lotService.UpdateLotAsync(id, dto));
    }
}
