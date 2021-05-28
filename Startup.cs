using HateoasDemo.Configuration;
using HateoasDemo.Configuration.LinkConfigurations;
using HateoasDemo.Configuration.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HateoasDemo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IHateoasService, HateoasService>();
            services.AddControllersWithViews(options => options.Filters.Add(typeof(HateoasResultFilter)));

            services.ConfigureOptions<ThoughtLinksDtoConfiguration>();
            services.ConfigureOptions<ThoughtListDtoLinksConfiguration>();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "HateoasDemo", Version = "v1" }); });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdorpDemo v1"));
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
