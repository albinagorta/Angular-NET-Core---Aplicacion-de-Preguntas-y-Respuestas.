using AppBackEnd.Domain.IRepositories;
using AppBackEnd.Domain.IServices;
using AppBackEnd.Persistence.Context;
using AppBackEnd.Persistence.Repositories;
using AppBackEnd.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using AppBackEnd.Utils;

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
            

            services.AddControllers().AddNewtonsoftJson(options =>
                                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                            );

            services.AddEndpointsApiExplorer();

            var conexionmysql = Configuration.GetConnectionString("conexionMysql");
            services.AddDbContext<AplicationDbContext>(options => 
            options.UseMySql(conexionmysql, ServerVersion.AutoDetect(conexionmysql)));

            //services.AddDbContext<AplicationDbContext>(options => 
            //               options.UseSqlServer(Configuration.GetConnectionString("conexionSqlserver")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ApiPreguntas",
                    Version = "v1",
                    Description = "Este es un web api",
                    Contact = new OpenApiContact
                    {
                        Email = "avideait@gmail.com",
                        Name = "Angel Albinagorta",
                        Url = new Uri("https://avideait.pw")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT"
                    }
                });

                //JWT
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });


            // Add Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["Jwt:Issuer"],
                            ValidAudience = Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                            ClockSkew = TimeSpan.Zero
                        });


            services.AddAutoMapper(typeof(Startup));

            // Cors
            services.AddCors(opciones => opciones.AddPolicy("AllowWebapp",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));


            

            // Service
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ICuestionarioService, CuestionarioService>();
            services.AddScoped<IRespuestaCuestionarioService, RespuestaCuestionarioService>();

            // Repository
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<ICuestionarioRepository, CuestionarioRepository>();
            services.AddScoped<IRespuestaCuestionarioRepository, RespuestaCuestionarioRepository>();

           
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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
