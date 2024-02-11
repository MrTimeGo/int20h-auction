namespace Auction.WebApi.Dto.Lot;

public class LotSort
{
    public LotSortType? Type { get; set; }
    public LotSortOrder? SortOrder { get; set; }
    public BetStepOrder? BetStepOrder { get; set; }
}

public enum LotSortType
{
    StartingAt,
    ClosingAt,
}

public enum LotSortOrder
{
    Ascending,
    Descending,
}

public enum BetStepOrder
{
    Ascending,
    Descending,
}