using AutoMapper;
using CommandAPI.Dtos;
using CommandDAL.Models;

namespace CommandAPI.Profiles
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