using AutoMapper;
using AccountManager.Application.DTOs; 
using AccountManager.Request;
using AccountManager.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<LoginRequest, LoginDto>();
        CreateMap<LoginDto, LoginRequest>();

        CreateMap<AccountDataRequest, RegisterDto>();
        CreateMap<RegisterDto, Account>();

    }
}
