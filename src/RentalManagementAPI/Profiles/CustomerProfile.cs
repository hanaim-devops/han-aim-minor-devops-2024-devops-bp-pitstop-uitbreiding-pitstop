using AutoMapper;
using Pitstop.RentalManagementAPI.Models;
using Pitstop.RentalManagementAPI.Responses;
using RentalManagementAPI.Models;

namespace Pitstop.RentalCarManagementAPI.MappingProfiles;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerResponse>();
    }
}