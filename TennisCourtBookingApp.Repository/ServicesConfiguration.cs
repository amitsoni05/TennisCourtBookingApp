using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TennisCourtBookingApp.Common.Utility;
using TennisCourtBookingApp.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisCourtBookingApp.Repository
{
    public static class ServicesConfiguration
    {
        public static void AddRepositoryService(this IServiceCollection services, IConfiguration configuration)
        {
            AppCommon.ConnectionString = configuration.GetConnectionString("connectionString");

            services.AddDbContext<TestSPC5Context>(options => options.UseSqlServer(AppCommon.ConnectionString).UseLazyLoadingProxies().EnableSensitiveDataLogging());
        }
    }
}
