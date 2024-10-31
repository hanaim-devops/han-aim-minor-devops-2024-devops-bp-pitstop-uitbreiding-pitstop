using AutoMapper;
using Pitstop.RentalManagementAPI.Responses;
using RentalManagementAPI.Models;

namespace Pitstop.RentalCarManagementAPI.MappingProfiles;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<Brand, BrandResponse>();
    }
}