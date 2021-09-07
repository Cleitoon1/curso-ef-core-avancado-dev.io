using Curso.Dominando.EFCore.Modulo18.UOWRepository.Data;
using Curso.Dominando.EFCore.Modulo18.UOWRepository.Data.Repositories;
using Curso.Dominando.EFCore.Modulo18.UOWRepository.Domain;
using Curso.Dominando.EFCore.Modulo18.UOWRepository.Data;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.Dominando.EFCore.Modulo18.UOWRepository
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

            services.AddControllers().AddNewtonsoftJson(_ =>
            {
                _.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Curso.Dominando.EFCore.Modulo18.UOWRepository", Version = "v1" });
            });

            services.AddDbContext<ApplicationContext>(_ =>
                _.UseSqlServer("Data Source=localhost; Initial Catalog=Uow; Integrated Security=true; MultipleActiveResultSets=true"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso.Dominando.EFCore.Modulo18.UOWRepository v1"));
            }
            InicializarBaseDeDados(app);
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InicializarBaseDeDados(IApplicationBuilder app)
        {
            using var db = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ApplicationContext>();

            if(db.Database.EnsureCreated())
            {
                db.Departamentos.AddRange(Enumerable.Range(1, 10).Select(_ => new Departamento
                {
                    Descricao = $"Departamento {_}",
                    Colaboradores = Enumerable.Range(1, 10).Select(__ => new Colaborador
                    {
                        Nome = $"Colaborador {__}"
                    }).ToList()
                }));
                db.SaveChanges();
            }
        }
    }
}
