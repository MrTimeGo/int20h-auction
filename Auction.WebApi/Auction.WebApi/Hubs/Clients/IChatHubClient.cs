using Auction.WebApi.Dto.Message;

namespace Auction.WebApi.Hubs.Clients;

public interface IChatHubClient
{
    Task SendMessage(Guid lotId, MessageDto message);
}
