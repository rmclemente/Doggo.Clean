using AutoMapper;
using Doggo.Application.Dtos;
using Doggo.Domain.Entities;

namespace Doggo.Application.AutoMapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Breed, BreedDto>().ReverseMap();
        }
    }
}
