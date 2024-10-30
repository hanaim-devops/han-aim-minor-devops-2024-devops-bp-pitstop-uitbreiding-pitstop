using AutoMapper;
using Pitstop.RentalCarManagementAPI.Events;
using RentalCarManagementAPI.Models;

namespace Pitstop.RentalCarManagementAPI.MappingProfiles;

public class RentalCarProfile : Profile
{
    public RentalCarProfile()
    {
        CreateMap<RentalCarRegistered, RentalCar>();
    }
}