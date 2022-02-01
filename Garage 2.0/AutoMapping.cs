using AutoMapper;
using Garage_2._0.Models.Entities;
using Garage_2._0.Models.ViewModels;

namespace Garage_2._0;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Vehicle, VehicleIndexViewModel>(); // means you want to map from Vehicle to VehicleIndexViewModel
    }
}