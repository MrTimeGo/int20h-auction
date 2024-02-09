using System.Net;

namespace Auction.WebApi.Expections;

public class HttpException(string message, HttpStatusCode status) : Exception(message)
{
    public HttpStatusCode StatusCode { get; set; } = status;
}
