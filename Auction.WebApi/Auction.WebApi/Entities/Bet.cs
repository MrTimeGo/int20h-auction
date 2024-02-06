namespace Auction.WebApi.Entities;

public class Bet : BaseEntity
{
    public decimal Amount { get; set; }

    public Guid LotId { get; set; }
    public Lot Lot { get; set; } = null!;

    public Guid AuthorId { get; set; }
    public User Author { get; set; } = null!;
}
