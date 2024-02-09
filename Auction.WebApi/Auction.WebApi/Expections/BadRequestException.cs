using System.Net;

namespace Auction.WebApi.Expections;

public class BadRequestException(string message) : HttpException(message, HttpStatusCode.BadRequest)
{
}
