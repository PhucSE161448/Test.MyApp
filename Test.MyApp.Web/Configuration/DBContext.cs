using Autofac;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Test.MyApp.Domain.Common;
using Test.MyApp.Domain.EntityModels;

namespace Test.MyApp.Web.Configuration
{
    public static class DBContext
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<MyAppDbContext>(options => options.UseSqlServer(AppConfiguration.ConnectionString.DefaultConnection));
            return services;
        }

        public static ContainerBuilder AddDbContext(this ContainerBuilder builder)
        {
            builder.Register(c => new SqlConnection(AppConfiguration.ConnectionString.DefaultConnection))
                .As<IDbConnection>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MyAppDbContext>().As<DbContext>().InstancePerLifetimeScope();
            return builder;
        }
    }
}
