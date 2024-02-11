using Auction.WebApi.Dto.Message;

namespace Auction.WebApi.Services.Interfaces;

public interface IChatService
{
    Task SendMessageForLot(Guid lotId, CreateMessageDto message);

    Task<List<MessageDto>> GetAllMessagesForLot(Guid lotId);
}
