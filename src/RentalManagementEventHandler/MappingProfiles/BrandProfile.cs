using AutoMapper;
using Pitstop.RentalCarManagementAPI.Events;
using RentalCarManagementAPI.Models;

namespace Pitstop.RentalCarManagementAPI.MappingProfiles;

public class BrandProfile : Profile
{
    public BrandProfile()
    {
        CreateMap<BrandRegistered, Brand>();
    }
}