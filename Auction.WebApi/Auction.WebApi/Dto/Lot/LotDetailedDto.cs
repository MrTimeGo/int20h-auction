using Auction.WebApi.Dto.Bet;

namespace Auction.WebApi.Dto.Lot;

public class LotDetailedDto : LotDto
{
    public ICollection<BetDto> Bets { get; set; } = new List<BetDto>();
}
