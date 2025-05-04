using TennisCourtBookingApp.Repository.Models;
using AutoMapper;
using TennisCourtBookingApp.Provider.IProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourtBookingApp.Common.BusinessEntities;

namespace TennisCourtBookingApp.Provider.Mapping
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {
            CreateMap<TennisCourtBookingModel, TennisCourtBooking>();
            CreateMap<TennisCourtBooking, TennisCourtBookingModel>();
            CreateMap<TennisCourtBookingRoleModel, TennisCourtBookingRole>();
            CreateMap<TennisCourtBookingRole, TennisCourtBookingRoleModel>();
            CreateMap<TennisCourtBookingUserModel, TennisCourtBookingUser>();
                 CreateMap<TennisCourtBookingUser, TennisCourtBookingUserModel>();
            CreateMap<TennisCourtModel, TennisCourt>();
            CreateMap<TennisCourt, TennisCourtModel>();




        }

    }
}
