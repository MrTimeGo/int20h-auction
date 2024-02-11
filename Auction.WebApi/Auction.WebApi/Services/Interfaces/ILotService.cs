using Auction.WebApi.Dto;
using Auction.WebApi.Dto.Lot;
using Auction.WebApi.Dto.Tag;

namespace Auction.WebApi.Services.Interfaces;

public interface ILotService
{
    Task<PaginationResult<LotDto>> GetLotsAsync(string? searchTerm, LotFilter filter, LotSort sort, PaginationModel pagination);
    Task<LotDto> CreateLotAsync(CreateLotDto dto);
}
