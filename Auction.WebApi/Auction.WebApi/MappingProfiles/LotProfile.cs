using Auction.WebApi.Dto.Bet;
using Auction.WebApi.Dto.Lot;
using Auction.WebApi.Entities;
using AutoMapper;

namespace Auction.WebApi.MappingProfiles;

public class LotProfile : Profile
{
    public LotProfile() 
    {
        CreateMap<Lot, LotDto>()
            .ForMember(l => l.status, (options) => options.MapFrom(l =>
                (l.StartingAt < DateTime.UtcNow && DateTime.UtcNow < l.ClosingAt) ? LotStatus.Active :
                (l.StartingAt > DateTime.UtcNow ? LotStatus.NotStarted : LotStatus.Closed)
            ))
            .ForMember(l => l.Tags, (options) => options.MapFrom(l => l.Tags.Select(t => t.Name)))
            .ForMember(l => l.Images, (options) => options.MapFrom(l => l.Images.Select(t => t.FilePath)))
            .ForMember(l => l.Author, (opions) => opions.MapFrom(l => l.Author.UserName));

        CreateMap<Lot, LotDetailedDto>()
            .ForMember(l => l.status, (options) => options.MapFrom(l =>
                (l.StartingAt < DateTime.UtcNow && DateTime.UtcNow < l.ClosingAt) ? LotStatus.Active :
                (l.StartingAt > DateTime.UtcNow ? LotStatus.NotStarted : LotStatus.Closed)
            ))
            .ForMember(l => l.Tags, (options) => options.MapFrom(l => l.Tags.Select(t => t.Name)))
            .ForMember(l => l.Images, (options) => options.MapFrom(l => l.Images.Select(t => t.FilePath)));

        CreateMap<Bet, BetDto>()
            .ForMember(b => b.Author, (options) => options.MapFrom(b => b.Author.UserName));

        CreateMap<CreateLotDto, Lot>()
            .ForMember(l => l.Tags, options => options.Ignore())
            .ForMember(l => l.Images, options => options.Ignore());
    }
}
