using AutoMapper;
using Pitstop.RentalManagementAPI.Commands;
using Pitstop.RentalManagementAPI.Models;
using Pitstop.RentalManagementAPI.Responses;
using RentalManagementAPI.Models;

namespace Pitstop.RentalCarManagementAPI.MappingProfiles;

public class RentalReservationProfile : Profile
{
    public RentalReservationProfile()
    {
        CreateMap<MakeReservation, RentalReservation>();
        CreateMap<RentalReservation, RentalReservationResponse>();
    }
}