using System.Net;

namespace Auction.WebApi.Expections;

public class ConfictExeption(string message) : HttpException(message, HttpStatusCode.Conflict)
{
}
