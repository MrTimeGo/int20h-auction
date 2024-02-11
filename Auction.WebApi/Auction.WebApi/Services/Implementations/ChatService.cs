using Auction.WebApi.Data;
using Auction.WebApi.Dto.Message;
using Auction.WebApi.Entities;
using Auction.WebApi.Expections;
using Auction.WebApi.Hubs;
using Auction.WebApi.Hubs.Clients;
using Auction.WebApi.Services.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Auction.WebApi.Services.Implementations;

public class ChatService(ICurrentUserService currentUser, AuctionContext context, UserManager<User> userManager, IHubContext<ChatHub, IChatHubClient> chatHubContext, IMapper mapper) : IChatService
{
    public async Task<List<MessageDto>> GetAllMessagesForLot(Guid lotId)
    {
        return await context.Messages
            .Include(m => m.Author)
            .Where(m => m.LotId == lotId)
            .ProjectTo<MessageDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task SendMessageForLot(Guid lotId, CreateMessageDto dto)
    {
        var lot = await context.Lots.FirstOrDefaultAsync(l => l.Id == lotId);

        if (lot is null)
        {
            throw new NotFoundExeption($"Lot with id {lotId} not found");
        }

        var entity = new Message()
        {
            LotId = lotId,
            AuthorId = currentUser.CurrentUserId!.Value,
            Text = dto.Text
        };

        context.Messages.Add(entity);
        await context.SaveChangesAsync();

        await chatHubContext.Clients.All.SendMessage(lotId, new MessageDto()
        {
            Text = entity.Text,
            Author = (await userManager.FindByIdAsync(entity.AuthorId.ToString()))!.UserName!,
            CreateadAt = DateTime.UtcNow,
        });
    }
}
