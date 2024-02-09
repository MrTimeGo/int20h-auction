namespace Auction.WebApi.Dto.Lot;

public class CreateLotDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartingAt { get; set; }
    public DateTime ClosingAt { get; set; }
    public decimal InitialPrice { get; set; }
    public decimal MinimalStep { get; set; }
    public List<Guid> Images { get; set; } = [];
    public List<string> Tags { get; set; } = [];
}
