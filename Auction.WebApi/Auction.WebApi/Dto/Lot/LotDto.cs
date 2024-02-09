namespace Auction.WebApi.Dto.Lot;
public class LotDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartingAt { get; set; }
    public DateTime ClosingAt { get; set; }
    public decimal InitialPrice { get; set; }
    public decimal MinimalStep {  get; set; }
    public List<string> Images { get; set; } = [];
    public List<string> Tags { get; set; } = [];
    public LotStatus status { get; set; }
}

public enum LotStatus
{
    NotStarted,
    Active,
    Closed
}
