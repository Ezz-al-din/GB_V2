using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.Test;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Mark, MarkToReturnDto>()
            .ForMember(d => d.MarkBrand, o => o.MapFrom(s => s.MarkBrand.Name))
            .ForMember(d => d.MarkType, o => o.MapFrom(s => s.MarkType.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<MarkUrlReslover>());
        }

    }
}