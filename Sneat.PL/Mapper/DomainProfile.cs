using AutoMapper;
using Sneat.DAL.Entity;
using Sneat.PL.ViewModel;

namespace Sneat.PL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Employee,EmployeeViewModel>().ReverseMap();

            CreateMap<Department,DepartmentViewModel>().ReverseMap();
        }
    }
}
