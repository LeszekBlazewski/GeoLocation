using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoLocation.BL.RepositoryInterfaces;
using GeoLocation.BL.Services;
using GeoLocation.BL.Services.ServicesInterfaces;
using GeoLocation.DAL;
using GeoLocation.DAL.Configs;
using GeoLocation.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
