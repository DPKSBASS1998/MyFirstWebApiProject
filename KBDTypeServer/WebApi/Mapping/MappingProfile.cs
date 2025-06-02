using AutoMapper;
using KBDTypeServer.Application.DTOs;
using KBDTypeServer.Domain.Entities;
using KBDTypeServer.WebApi.ViewModels;

namespace KBDTypeServer.WebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginViewModel, LoginDto>();
            CreateMap<RegisterViewModel, RegisterDto>();
        }
    }

}
