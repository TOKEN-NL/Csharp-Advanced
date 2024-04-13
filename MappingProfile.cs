using AutoMapper;
using Csharp_Advanced.Models;
using System.Linq;

namespace Csharp_Advanced
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Location, LocationDto>()
                .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => GetCoverImageUrl(src)))
                .ForMember(dest => dest.LandlordAvatarURL, opt => opt.MapFrom(src => GetLandlordAvatarUrl(src)));
        }

        private string GetCoverImageUrl(Location src)
        {
            var coverImage = src.Images.FirstOrDefault(i => i.IsCover);
            return coverImage != null ? coverImage.Url : null;
        }

        private string GetLandlordAvatarUrl(Location src)
        {
            if (src.Landlord != null && src.Landlord.Avatar != null)
            {
                return src.Landlord.Avatar.Url;
            }
            return null;
        }
    }
}
