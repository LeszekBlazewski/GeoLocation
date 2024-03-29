using GeoLocation.BL.RepositoryInterfaces;
using GeoLocation.BL.Services;
using GeoLocation.BL.Services.ServicesInterfaces;
using GeoLocation.DAL;
using GeoLocation.DAL.Configs;
using GeoLocation.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AutoMapper;

namespace GeoLocation.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new ServerConfig();
            Configuration.Bind(config);

            var ipAddressDetailsContext = new DatabaseContext(config.MongoDB);

            var ipAddressDetailRepository = new IpAddressDetailsRepository(ipAddressDetailsContext);

            services.AddSingleton<IIpAddressDetailsRepository>(ipAddressDetailRepository);

            var ipAddressDetailsService = new IpAddressDetailsService(ipAddressDetailRepository);

            services.AddSingleton<IIpAddressDetailsService>(ipAddressDetailsService);

            services.AddScoped<IIpStackService, IpStackService>();

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeoLocations API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeoLocationsApi");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
