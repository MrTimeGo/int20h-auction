using Auction.WebApi.Dto.Bet;

namespace Auction.WebApi.Hubs.Clients;

public interface IBetHubClient
{
    Task SendBetMadeNotification(Guid lotId, BetDto bet);
}
