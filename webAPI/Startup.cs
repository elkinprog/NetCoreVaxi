using Aplicacion.Cursos;
using MediatR;
using Microsoft.AspNetCore.Builder;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistencia;
using FluentValidation.AspNetCore;
using Aplicacion;
using webAPI.Middleware;

namespace webAPI
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

            services.AddDbContext<ConnectionContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultCoonection")));

            services.AddMediatR(typeof(Consulta.Manejador).Assembly);

            services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());


            services.AddSwaggerGen(c => {
               c.SwaggerDoc("v1", new OpenApiInfo { 
                Title = "Servicios para mantenimiento de cursos", 
                Version = "v1" 
            });
            c.CustomSchemaIds(c=> c.FullName);

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseMiddleware<ManejadorErrorMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

             app.UseSwagger();
             app.UseSwaggerUI(c => {
                 c.SwaggerEndpoint("/swagger/v1/swagger.json","Cursos Online V1");
             });
 
        }
    }
}

