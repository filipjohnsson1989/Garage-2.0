using AutoMapper;
using Garage_2._0.Models.Entities;
using Garage_2._0.Models.ViewModels;

namespace Garage_2._0;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<Vehicle, ParkingDetailModel>(); // means you want to map from Vehicle to ParkingDetailModel
        CreateMap<Vehicle, OverviewModel>(); // means you want to map from Vehicle to OverviewModel
        CreateMap<Vehicle, TicketViewModel>(); // means you want to map from Vehicle to OverviewModel


    }
}