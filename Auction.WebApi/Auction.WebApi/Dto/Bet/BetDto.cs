namespace Auction.WebApi.Dto.Bet;

public class BetDto
{
    public string Author { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}
