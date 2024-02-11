using Auction.WebApi.Hubs.Clients;
using Microsoft.AspNetCore.SignalR;

namespace Auction.WebApi.Hubs;

public class ChatHub : Hub<IChatHubClient>
{
}
