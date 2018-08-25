using AutoMapper;
using DbEntities.Models;
using DTOs.ViewModels;
using System.Linq;

namespace DTOs.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Department, DepartmentViewModel>()
                .ForMember(d => d.IsSelected, cfg => cfg.Ignore())
                .ForMember(d => d.EmployeesCount, cfg => cfg.MapFrom(c => c.Employees.Count()))
                .ForMember(d => d.Id, cfg => cfg.MapFrom(c => c.Id))
                .ForMember(d => d.Name, cfg => cfg.MapFrom(c => c.Name));

            CreateMap<EmployeePosition, PositionViewModel>()
                .ForMember(p => p.EmployeesCount, cfg => cfg.MapFrom(p => p.Employees.Count()))
                .ForMember(p => p.Id, cfg => cfg.MapFrom(c => c.Id))
                .ForMember(p => p.Name, cfg => cfg.MapFrom(c => c.Name));

            CreateMap<EmployeeUser, EmployeeViewModel>()
                .ForMember(e => e.Department, cfg => cfg.MapFrom(e => e.Department.Name));

            CreateMap<Request, RequestViewModel>()
                .ForMember(r => r.Name, cfg => cfg.MapFrom(r => $"{r.EmployeeUser.FirstName} {r.EmployeeUser.LastName}"));
        }
    }
}
