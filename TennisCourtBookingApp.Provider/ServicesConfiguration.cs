using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TennisCourtBookingApp.Provider.IProvider;
using TennisCourtBookingApp.Provider.Mapping;
using TennisCourtBookingApp.Provider.Provider;
using TennisCourtBookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisCourtBookingApp.Common.Utility;

namespace TennisCourtBookingApp.Provider
{
    public static class ServicesConfiguration
    {

        public static void AddProviderServices(this IServiceCollection services, IConfiguration configuration)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddRepositoryService(configuration);
           
            services.AddTransient<IBookingProvider, BookingProvider>();
            services.AddTransient<ICommonProvider, CommonProvider>();
            services.AddTransient<ITennisCourtProvider, TennisCourtProvider>();
            services.AddTransient<IUserProvider, UserProvider>();
            services.AddTransient<ISessionManager, SessionManager>();

            #region Provider Injection

            #endregion
        }
    }
}
