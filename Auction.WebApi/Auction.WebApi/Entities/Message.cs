namespace Auction.WebApi.Entities;

public class Message : BaseEntity
{
    public string Text { get; set; } = string.Empty;

    public Guid LotId { get; set; }
    public Lot Lot { get; set; } = null!;

    public Guid AuthorId { get; set; }
    public User Author { get; set; } = null!;
}
