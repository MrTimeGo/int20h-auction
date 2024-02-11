namespace Auction.WebApi.Dto.Message;

public class MessageDto
{
    public string Author { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public DateTime CreateadAt { get; set; }
}
