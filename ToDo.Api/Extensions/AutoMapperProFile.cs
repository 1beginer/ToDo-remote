using AutoMapper;
using ToDo.Api.Context.Models;
using ToDo.Shared.Dtos;

namespace ToDo.Api.Extensions
{
    public class AutoMapperProFile : MapperConfigurationExpression
    {
        public AutoMapperProFile()
        {
            CreateMap<ToDoE, ToDoDto>().ReverseMap();
            CreateMap<Memo, MemoDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
