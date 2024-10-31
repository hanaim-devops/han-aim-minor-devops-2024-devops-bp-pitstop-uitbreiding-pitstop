using AutoMapper;
using Pitstop.RentalManagementAPI.Responses;
using RentalManagementAPI.Models;

namespace Pitstop.RentalCarManagementAPI.MappingProfiles;

public class RentalCarProfile : Profile
{
    public RentalCarProfile()
    {
        CreateMap<RentalCar, RentalCarResponse>();
    }
}