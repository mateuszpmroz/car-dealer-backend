using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealer.Infrastructure.Bundles.Advert.Service;
using CarDealer.Infrastructure.Bundles.Advert.Repository;
using CarDealer.Infrastructure.Bundles.Car.Repository;
using CarDealer.Infrastructure.Bundles.Car.Service;
using CarDealer.Infrastructure.Bundles.User.Repository;
using CarDealer.Infrastructure.Bundles.User.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CarDealer.Infrastructure.Context;
using Swashbuckle.AspNetCore.Swagger;

namespace CarDealer
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
            services.AddCors(); // Make sure you call this previous to AddMvc
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<IAdvertRepository, AdvertRepository>();
            services.AddScoped<IAdvertService, AdvertService>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddDbContext<CarDealerContext>(options =>
                options.UseSqlite("DataSource=dbo.CarDealerApi.db",
                    builder => builder.MigrationsAssembly("CarDealer.Infrastructure")));

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "CarDealerApi", Version = "V1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }
            app.UseCors(
                options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
            );

            //app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My CarDealerApi V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}