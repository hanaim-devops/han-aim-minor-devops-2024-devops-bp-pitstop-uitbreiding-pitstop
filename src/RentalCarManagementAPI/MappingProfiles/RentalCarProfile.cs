using AutoMapper;
using Pitstop.RentalCarManagementAPI.Commands;
using Pitstop.RentalCarManagementAPI.Events;
using RentalCarManagementAPI.Models;

namespace Pitstop.RentalCarManagementAPI.MappingProfiles;

public class RentalCarProfile : Profile
{
    public RentalCarProfile()
    {
        CreateMap<RentalCar, RentalCarRegistered>();
    }
}