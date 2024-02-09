using System.Net;

namespace Auction.WebApi.Expections;

public class NotFoundExeption(string message) : HttpException(message, HttpStatusCode.NotFound)
{
}
