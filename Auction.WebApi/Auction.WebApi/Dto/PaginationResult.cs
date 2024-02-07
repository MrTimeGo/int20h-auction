namespace Auction.WebApi.Dto;

public class PaginationResult<T>
{
    public List<T> Entities { get; set; } = [];
    public int Count { get; set; }
}
