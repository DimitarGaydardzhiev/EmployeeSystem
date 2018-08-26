using AutoMapper;
using DbEntities.Models;
using DTOs.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace DTOs.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Department, DepartmentDto>()
                .ForMember(d => d.IsSelected, cfg => cfg.Ignore())
                .ForMember(d => d.EmployeesCount, cfg => cfg.MapFrom(c => c.Employees.Count()))
                .ForMember(d => d.Id, cfg => cfg.MapFrom(c => c.Id))
                .ForMember(d => d.Name, cfg => cfg.MapFrom(c => c.Name));

            CreateMap<EmployeePosition, PositionDto>()
                .ForMember(p => p.EmployeesCount, cfg => cfg.MapFrom(p => p.Employees.Count()))
                .ForMember(p => p.Id, cfg => cfg.MapFrom(c => c.Id))
                .ForMember(p => p.Name, cfg => cfg.MapFrom(c => c.Name));

            CreateMap<EmployeeUser, EmployeeDto>()
                .ForMember(e => e.Department, cfg => cfg.MapFrom(e => e.Department.Name))
                .ForMember(e => e.StartingDate, cfg => cfg.MapFrom(e => e.InCompanyFrom))
                .ForMember(e => e.PositionId, cfg => cfg.MapFrom(e => e.EmployeePositionId))
                .ForMember(e => e.Position, cfg => cfg.MapFrom(e => e.EmployeePosition.Name));

            CreateMap<Request, RequestDto>()
                .ForMember(r => r.User, cfg => cfg.MapFrom(r => $"{r.EmployeeUser.FirstName} {r.EmployeeUser.LastName}"))
                .ForMember(r => r.RequestType, cfg => cfg.MapFrom(r => r.RequestType.Name));

            CreateMap<IdentityRole, RoleDto>();

            CreateMap<Project, ProjectDto>();
        }
    }
}
