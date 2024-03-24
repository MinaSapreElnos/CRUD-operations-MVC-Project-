using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;

namespace Demo.PL.mapingProfiles
{
    public class UserProfile :Profile

    {
        public UserProfile()
        {
            CreateMap<AppLication_User, UsersViewModel>().ReverseMap();  
        }
    }
}
