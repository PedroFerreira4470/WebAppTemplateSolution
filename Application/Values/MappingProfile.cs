using Application.Values.Queries.GetValuesList;
using AutoMapper;
using Domain.Entities;

namespace Application.Values
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Value, ValuesListDto>()
                .ForMember(destination => destination.Number, option => option.MapFrom(source => source.ValueNumber));
        }
    }
}
