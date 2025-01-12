using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using API.Comman;
using API.Domain;
using API.Infrastructure.Interfaces;
using API.Infrastructure.Repository;
using API.Services.Interfaces;
using API.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<DataContext>(option =>
            {
                option.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            return services;
        }

    }
}