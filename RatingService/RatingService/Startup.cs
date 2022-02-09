using System;
using DAL.Repositories;
using AutoMapper;
using Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Core.Interfaces.Directors;
using DAL.Directors;
using Core.DatabaseSettings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Core.Features;
using Core.Options;
using Services.Interfaces;
using Services.ServicesClasses;
using Ratezen.Auth.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Core.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DotNetOpenAuth.InfoCard;
using System.Security.Claims;

namespace RatingService
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
            services.AddControllers(setupAction => {
                setupAction.ReturnHttpNotAcceptable = true; // accept only application/json
            }).AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver
                = new CamelCasePropertyNamesContractResolver();
            }).AddXmlDataContractSerializerFormatters();

            //add swagger
            services.AddSwaggerGen();

            // CORS
            EnableCors(services);

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:Secret"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role , "Admin"));
                options.AddPolicy("Client", policy => policy.RequireClaim(ClaimTypes.Role, "Client"));
            });

            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
            services.Configure<EncryptionSettings>(Configuration.GetSection(nameof(EncryptionSettings)));
            services.Configure<MailSettings>(Configuration.GetSection(nameof(MailSettings)));
            services.Configure<AuthSettings>(Configuration.GetSection(nameof(AuthSettings)));

            services.AddSingleton<IDatabaseSettings>(x => x.GetRequiredService<IOptions<DatabaseSettings>>().Value);
            services.AddSingleton<IEncryptionSettings>(x => x.GetRequiredService<IOptions<EncryptionSettings>>().Value);
            services.AddSingleton<IEmailSettings>(x => x.GetRequiredService<IOptions<MailSettings>>().Value);
            services.AddSingleton<IAuthSettings>(x => x.GetRequiredService<IOptions<AuthSettings>>().Value);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //dependency injection repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();

            //dependency injection directors
            services.AddScoped<IRatingDirector, RatingDirector>();
            services.AddScoped<IUserDirector, UserDirector>();
            services.AddScoped<IMediaDirector, MediaDirector>();

            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IAuthService, AuthService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("LocalHost");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ratezen API");
            });
        }

        public void EnableCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
                options.AddPolicy("LocalHost",
                    builder => builder.WithOrigins("http://localhost:4200", "http://localhost:4201", "http://localhost:4202")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
            });
        }
    }
}
