using AutoMapper;
using Pitstop.RentalManagementAPI.Responses;
using RentalManagementAPI.Models;

namespace Pitstop.RentalCarManagementAPI.MappingProfiles;

public class ModelProfile : Profile
{
    public ModelProfile()
    {
        CreateMap<Model, ModelResponse>();
    }
}