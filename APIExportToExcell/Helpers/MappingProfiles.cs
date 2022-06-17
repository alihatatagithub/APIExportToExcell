using APIExportToExcell.DTOS;
using APIExportToExcell.Entities;
using AutoMapper;

namespace APIExportToExcell.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDto>();
            

        }
    }
}
