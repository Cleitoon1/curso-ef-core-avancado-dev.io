using Curso.Dominando.EFCore.Modulo17.MultiTenant.Data;
using Curso.Dominando.EFCore.Modulo17.MultiTenant.Domain;
using Curso.Dominando.EFCore.Modulo17.MultiTenant.Extensions;
using Curso.Dominando.EFCore.Modulo17.MultiTenant.Interceptors;
using Curso.Dominando.EFCore.Modulo17.MultiTenant.Middlwares;
using Curso.Dominando.EFCore.Modulo17.MultiTenant.ModelFactory;
using Curso.Dominando.EFCore.Modulo17.MultiTenant.Provider;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Curso.Dominando.EFCore.Modulo17.MultiTenant
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Curso.Dominando.EFCore.Modulo17.MultiTenant", Version = "v1" });
            });


            //Estratégia 1 - Identificador na tabela
            //services.AddScoped<StrategySchemaInterceptor>();
            //services.AddScoped<TenantData>();

            //services.AddDbContext<ApplicationContext>((provider, options) =>
            //{
            //    options
            //        .UseSqlServer("Data Source=localhost; Initial Catalog=EFCoreAvancadoTenant99; Integrated Security=true; MultipleActiveResultSets=true")
            //        .LogTo(Console.WriteLine)
            //        .EnableSensitiveDataLogging();
            //    var interceptor = provider.GetRequiredService<StrategySchemaInterceptor>();
            //    options.AddInterceptors(interceptor);
            //});


            //Estratégia 2 - Schema
            //services.AddScoped<TenantData>();
            //services.AddDbContext<ApplicationContext>((provider, options) =>
            //{
            //    options
            //        .UseSqlServer("Data Source=localhost; Initial Catalog=EFCoreAvancadoTenant99; Integrated Security=true; MultipleActiveResultSets=true")
            //        .LogTo(Console.WriteLine)
            //        .ReplaceService<IModelCacheKeyFactory, StrategySchemaModelCacheKey>()
            //        .EnableSensitiveDataLogging();
            //});

            //Estratégia 3 - Banco de Dados
            services.AddHttpContextAccessor();
            services.AddScoped<ApplicationContext>(provider => {
                var httpContext = provider.GetService<IHttpContextAccessor>()?.HttpContext;
                var tenantId = httpContext?.GetTenantId();
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
                //var connectionString = Configuration.GetConnectionString(tenantId);
                var connectionString = Configuration.GetConnectionString("custom").Replace("_DATABASE_", tenantId);
                optionsBuilder.UseSqlServer(connectionString)
                .LogTo(Console.WriteLine)
                .EnableSensitiveDataLogging();

                return new ApplicationContext(optionsBuilder.Options);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso.Dominando.EFCore.Modulo17.MultiTenant v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseMiddleware<TenantMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
