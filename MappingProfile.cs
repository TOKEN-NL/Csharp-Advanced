using AutoMapper;
using Csharp_Advanced.Models;
using System.Linq;

namespace Csharp_Advanced
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //V1 DTO
            CreateMap<Location, LocationDto>()
                .ForMember(dest => dest.imageURL, opt => opt.MapFrom(src => GetCoverImageUrl(src)))
                .ForMember(dest => dest.landlordAvatarURL, opt => opt.MapFrom(src => GetLandlordAvatarUrl(src)));

            //V2 DTO
            CreateMap<Location, LocationDtoV2>()
                .ForMember(dest => dest.imageURL, opt => opt.MapFrom(src => GetCoverImageUrl(src)))
                .ForMember(dest => dest.landlordAvatarURL, opt => opt.MapFrom(src => GetLandlordAvatarUrl(src)))
                .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.PricePerDay));
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
