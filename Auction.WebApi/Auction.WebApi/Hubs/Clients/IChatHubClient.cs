using Auction.WebApi.Dto.Message;

namespace Auction.WebApi.Hubs.Clients;

public interface IChatHubClient
{
    Task SendMessage(MessageDto message);
}
