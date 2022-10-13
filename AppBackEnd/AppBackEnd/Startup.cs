using AppBackEnd.Domain.IRepositories;
using AppBackEnd.Domain.IServices;
using AppBackEnd.Persistence.Context;
using AppBackEnd.Persistence.Repositories;
using AppBackEnd.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AppBackEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            var conexionmysql = Configuration.GetConnectionString("conexionMysql");
            services.AddDbContext<AplicationDbContext>(options => 
            options.UseMySql(conexionmysql, ServerVersion.AutoDetect(conexionmysql)));

            //services.AddDbContext<AplicationDbContext>(options => 
            //               options.UseSqlServer(Configuration.GetConnectionString("conexionSqlserver")));

            services.AddCors(opciones => opciones.AddPolicy("AllowWebapp",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

            // Service
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ILoginService, LoginService>();

            // Repository
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();

            
            services.AddSwaggerGen();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowWebapp");

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
