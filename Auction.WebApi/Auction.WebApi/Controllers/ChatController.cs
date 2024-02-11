using Auction.WebApi.Dto.Message;
using Auction.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Auction.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ChatController(IChatService chatService) : ControllerBase
{
    [HttpGet("lot/{id}")]
    public async Task<ActionResult<MessageDto>> GetMessagesForLot([FromRoute] Guid id)
    {
        return Ok(await chatService.GetAllMessagesForLot(id));
    }

    [HttpPost("lot/{id}")]
    public async Task<IActionResult> SendMessageToLotId([FromRoute] Guid id, [FromBody] CreateMessageDto message)
    {
        await chatService.SendMessageForLot(id, message);
        return Ok();
    }

}
