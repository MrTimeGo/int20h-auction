using System.Net;

namespace Auction.WebApi.Expections;

public class UnauthorizedExection(string message) : HttpException(message, HttpStatusCode.Unauthorized)
{
}
