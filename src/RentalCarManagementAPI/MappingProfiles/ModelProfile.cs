using AutoMapper;
using Pitstop.RentalCarManagementAPI.Events;
using RentalCarManagementAPI.Models;

namespace Pitstop.RentalCarManagementAPI.MappingProfiles;

public class ModelProfile : Profile
{
    public ModelProfile()
    {
        CreateMap<Model, ModelRegistered>();
    }
}