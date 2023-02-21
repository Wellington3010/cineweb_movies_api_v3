using cineweb_movies_api.Entities;
using cineweb_movies_api.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using cineweb_movies_api.Mapper;
using cineweb_movies_api.Context;
using System;

namespace cineweb_movies_api
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo());
            });

            services.AddControllers();
            services.AddScoped<FilmeBaseRepository<Filme, Guid>, FilmeRepository>();
            services.AddScoped<PedidoBaseRepository<Pedido, int>, PedidoRepository>();
            services.AddScoped<IngressoBaseRepository<Ingresso, int, Guid>, IngressoRepository>();
            services.AddScoped<ClienteBaseRepository<Cliente, int, string>, ClienteRepository>();

            services.AddAutoMapper(typeof(ConfigurationMapping));

            services.AddDbContext<ApplicationContext, ApplicationContext>(options =>
            {
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddCors(setup => {
                setup.AddPolicy("CorsPolicy", builder => {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoAPI V1");
            });

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
