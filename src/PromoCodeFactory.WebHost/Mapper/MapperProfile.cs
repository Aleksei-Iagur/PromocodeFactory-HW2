namespace PromoCodeFactory.WebHost.Mapper
{
    using AutoMapper;
    using PromoCodeFactory.Core.Domain.Administration;
    using PromoCodeFactory.WebHost.Models;
    using PromoCodeFactory.WebHost.Models.ModelsIn;
    using System;

    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeShortResponse>();
            CreateMap<Employee, EmployeeResponse>();
            CreateMap<Role, RoleItemResponse>();
            CreateMap<EmployeeForCreateDto, Employee>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap <RoleItemForCreateDto, Role>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
        }
    }
}
