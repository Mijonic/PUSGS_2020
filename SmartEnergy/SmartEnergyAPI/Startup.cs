using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

using SmartEnergy.Contract.Interfaces;
using SmartEnergy.Infrastructure;
using SmartEnergy.Service.Services;
using SmartEnergyAPI.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergyAPI
{
    public class Startup
    {
        private readonly string _cors = "cors";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<SmartEnergyDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SmartEnergyDatabase")));

            
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Smart Energy API", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: _cors, builder => {
                    builder.SetIsOriginAllowed(_ => true).AllowAnyHeader()
                                        .AllowAnyMethod().AllowCredentials();
                });
            });



            //Add Service implementations
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IIconService, IconService>();
            services.AddScoped<ICrewService, CrewService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IWorkRequestService, WorkRequestService>();
            services.AddScoped<IIncidentService, IncidentService>();
            services.AddScoped<IMultimediaService, MultimediaService>();
            services.AddScoped<ITimeService, TimeService>();
            services.AddScoped<IStateChangeService, StateChangeService>();
            services.AddScoped<IResolutionService, ResolutionService>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors(_cors);

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Smart Energy API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SmartEnergyDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
