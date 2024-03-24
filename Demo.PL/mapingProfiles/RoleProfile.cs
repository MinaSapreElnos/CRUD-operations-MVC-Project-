using AutoMapper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Demo.PL.mapingProfiles
{
    public class RoleProfile :Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleViewMOdel>()
                     .ForMember(d=>d.RoleName ,O=>O.MapFrom(S=>S.Name)).ReverseMap();  
        }
    }
}
