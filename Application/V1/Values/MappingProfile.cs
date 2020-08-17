using Application.V1.Values.Queries.GetValuesList;
using AutoMapper;
using Domain.Entities;

namespace Application.V1.Values
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Value, ValuesListResponseDto>()
                .ForMember(destination => destination.Number,
                    option => option.MapFrom(source => source.ValueNumber));
        }
    }
}
