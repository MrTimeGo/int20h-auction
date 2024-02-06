namespace Auction.WebApi.Entities;

public class Lot : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal InitialPrice { get; set; }
    public decimal MinimalStep { get; set; }
    public DateTime StartingAt { get; set; }
    public DateTime ClosingAt { get; set; }

    public Guid AuthorId { get; set; }
    public User Author { get; set; } = null!;

    public ICollection<StaticFile> Images { get; set; } = null!;

    public ICollection<Tag> Tags { get; set; } = null!;
}

