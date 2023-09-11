using AutoMapper;
using Framework.Core.AutoMapper;
using Framework.Identity.Data.Dtos;
using Framework.Identity.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace Framework.Identity.Data
{
    public class IdentityAutoMapperProfile : Profile, IMapperProfile
    {
        public IdentityAutoMapperProfile()
        {
            this.CreateMap<ApplicationUser, UserDto>().ReverseMap();
            this.CreateMap<UserUpdateDto, UserDto>().ReverseMap();
            this.CreateMap<ApplicationRole, RoleDto>().ReverseMap();
            this.CreateMap<ApplicationUserRoles, UserRolesDto>().ReverseMap();
            this.CreateMap<IdentityUserToken<Guid>, UserTokensDto>().ReverseMap();
        }

        public int Order { get; set; } = 1;
    }
}
