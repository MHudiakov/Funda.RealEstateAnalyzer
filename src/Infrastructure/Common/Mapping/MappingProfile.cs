using AutoMapper;
using Domain.Entities;
using Infrastructure.Clients.PropertyApi.Models;

namespace Infrastructure.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PropertyDto, Property>();
    }
}