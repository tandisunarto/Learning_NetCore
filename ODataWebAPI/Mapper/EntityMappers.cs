using AutoMapper;
using ODataWebAPI.Migrations;
using ODataWebAPI.Models;

namespace ODataWebAPI.Mapper
{
    public class EntityMappers : Profile
    {
        public EntityMappers()
        {
            CreateMap<EmployeeViewModel, Employees>().ReverseMap();
        }
    }
}