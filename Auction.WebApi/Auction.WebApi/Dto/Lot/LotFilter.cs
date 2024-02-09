namespace Auction.WebApi.Dto.Lot;

public class LotFilter
{
    public bool? MyLots { get; set; }
    public bool? MyBets { get; set; }
    public LotStatus? LotStatus { get; set; }
}
