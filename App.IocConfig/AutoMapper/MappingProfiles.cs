using App.Entities.Identity;
using App.ViewModels.Manage;
using App.ViewModels.RoleManager;
using App.ViewModels.UserManager;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.IocConfig.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {


            CreateMap<Role, RolesViewModel>().ReverseMap()
                    .ForMember(p => p.Users, opt => opt.Ignore())
                    .ForMember(p => p.Claims, opt => opt.Ignore());



            CreateMap<User, UsersViewModel>().ReverseMap()

                  .ForMember(p => p.Claims, opt => opt.Ignore());

            CreateMap<User, ProfileViewModel>().ReverseMap()

                   .ForMember(p => p.Claims, opt => opt.Ignore());



        }
    }
}
