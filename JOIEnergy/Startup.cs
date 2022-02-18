using FluentValidation;
using JOIEnergy.Dataset;
using JOIEnergy.Domain;
using JOIEnergy.Extensions;
using JOIEnergy.Filters;
using JOIEnergy.Middlewares;
using JOIEnergy.Services;
using JOIEnergy.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JOIEnergy
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
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(DummyExceptionFilter));
            });
            services.AddSwaggerGen();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IMeterReadingService, MeterReadingService>();
            services.AddScoped<IEnergyCostCalculationService, EneryCostCalclationService>();
            //services.AddValidatorsFromAssemblyContaining<MeterReadingsValidator>();
            //services.AddScoped<IValidator<MeterReadings>, MeterReadingsValidator>();
            services.AddSingleton<IDatasetService, DatasetService>();
            services.AddSingleton<IJWTTokenManagerService, JWTTokenManagerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Joi energy API V1");
                });
            }
            app.UseAuthentication();
            app.UseCustomMiddlewares();
            app.UseMvc();
        }
    }
}
