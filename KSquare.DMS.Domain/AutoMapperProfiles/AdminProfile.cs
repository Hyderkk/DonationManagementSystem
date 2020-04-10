using AutoMapper;
using KSquare.DMS.Domain.Entities;
using KSquare.DMS.Domain.Models;

namespace KSquare.DMS.Domain.AutoMapperProfiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Beneficiary, BeneficiaryModel>().ReverseMap();

            CreateMap<ClientModel, Client>().ReverseMap();
            CreateMap<Client, ClientModel>().ReverseMap();
            CreateMap<CustomerModel, Customer>().ReverseMap();
            CreateMap<Customer, CustomerModel>().ReverseMap();
        }
    }
}
