
using Auction.WebApi.Dto.File;
using Auction.WebApi.Entities;
using AutoMapper;

namespace Auction.WebApi.MappingProfiles
{
    public class FileProfile: Profile
    {
        public FileProfile()
        {
            CreateMap<StaticFile, FileDto>().ForMember(f=>f.Url, options=>options.MapFrom(f=>f.FilePath));
        }
    }
}
