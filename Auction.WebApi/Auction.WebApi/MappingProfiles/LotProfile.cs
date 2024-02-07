using Auction.WebApi.Dto.Lot;
using Auction.WebApi.Entities;
using AutoMapper;

namespace Auction.WebApi.MappingProfiles;

public class LotProfile : Profile
{
    public LotProfile() 
    {
        CreateMap<Lot, LotDto>()
            .ForMember(l => l.LotStatus, (options) => options.MapFrom(l =>
                (l.StartingAt < DateTime.UtcNow && DateTime.UtcNow < l.ClosingAt) ? LotStatus.Active :
                (l.StartingAt < DateTime.UtcNow ? LotStatus.NotStarted : LotStatus.Closed)
            ));
    }
}
