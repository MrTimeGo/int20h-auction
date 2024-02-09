using System.Net;

namespace Auction.WebApi.Expections;

public class ForbiddenExeption(string message) : HttpException(message, HttpStatusCode.Forbidden)
{
}
