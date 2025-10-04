using System;
using AutoMapper;

namespace Demo1.Profiles;

public class EmployeeProfile: Profile
{
    public EmployeeProfile()
    {
        // CreateMap<Source, Destination>();
        CreateMap<Models.Employee, Models.DTOs.EmployeeDTO>();
        CreateMap<Models.DTOs.EmployeeAddDTO, Models.Employee>();
    }
}
