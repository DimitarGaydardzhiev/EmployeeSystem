﻿//using AutoMapper;
//using DbEntities.Models;
//using DTOs.ViewModels;
//using System.Linq;

//namespace DTOs.AutoMapper
//{
//    public class AutoMapperProfile : Profile
//    {
//        public AutoMapperProfile()
//        {
//            CreateMap<Department, DepartmentViewModel>()
//                .ForMember(d => d.IsSelected, cfg => cfg.Ignore())
//                .ForMember(d => d.EmployeesCount, cfg => cfg.MapFrom(c => c.Employees.Count()))
//                .ForMember(d => d.Id, cfg => cfg.MapFrom(c => c.Id))
//                .ForMember(d => d.Name, cfg => cfg.MapFrom(c => c.Name));

//        }
//    }
//}
