using Auction.WebApi.Dto.Message;
using Auction.WebApi.Entities;
using AutoMapper;

namespace Auction.WebApi.MappingProfiles;

public class MessageProfile : Profile
{
    public MessageProfile()
    {
        CreateMap<Message, MessageDto>()
            .ForMember(m => m.Author, options => options.MapFrom(m => m.Author.UserName));
    }
}
