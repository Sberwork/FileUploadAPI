using AutoMapper;
using CommandBLL.Models;
using CommandDAL.Models;

namespace CommandBLL.Profiles
{
    public class CommandsProfile : Profile
    {
        //Source ➤ Target
        public CommandsProfile()
        {
            CreateMap<Command, CommandReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<CommandUpdateDto, Command>();
            CreateMap<Command, CommandUpdateDto>();
        }
    }
}