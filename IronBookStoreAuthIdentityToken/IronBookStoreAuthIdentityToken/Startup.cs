using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IronBookStoreAuthIdentityToken.Authorization;
using IronBookStoreAuthIdentityToken.Core.Entities;
using IronBookStoreAuthIdentityToken.Core.Services;
using IronBookStoreAuthIdentityToken.Infraestructure;
using IronBookStoreAuthIdentityToken.Infraestructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace IronBookStoreAuthIdentityToken
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
            var jwtSettings = GetJwtSettings();
            services.AddControllers();

            //TODO: Add Identity Configuration
            services.AddIdentity<User, IdentityRole>(cfg =>
            {
                //TODO: Add Identity Secure policies
                cfg.User.RequireUniqueEmail = true;
                cfg.Password.RequiredLength = 10;
                cfg.SignIn.RequireConfirmedAccount = true;
                cfg.SignIn.RequireConfirmedEmail = true;
            })//TODO: Specify Identity Store (it could be different to Business Store)
            .AddEntityFrameworkStores<ApplicationDbContext>();

            //TODO: Add JWT Configuration Token
            /*
             * Register Jwt as the Authentication service
             * Register our Authentication Schema --> We use JWT: JwtBearerDefaults.AuthenticationScheme
             * Configure our Authentication Schema --> We specify needed parameters for consider a token valid
             */

            /*
             * TODO: Order AddAuthentication & AddIdentity  
             * If we want to use: services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) because we have only one scheme different to cookies (default)
             * We need to declare:  services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) BEFORE AddIdentity() because AddIdentity() adding a lot of cookies configuration
             * However if we set JwtBearerDefaults.AuthenticationScheme as default before AddIdentity() there is not any problem.
            */
            services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                        ValidateIssuerSigningKey = true, //verify that the key used to sign the incoming token is part of a list of trusted keys
                        ValidateIssuer = true, //validate the server that created that token
                        ValidIssuer = jwtSettings.Issuer,

                        ValidateAudience = true, //ensure that the recipient of the token is authorized to receive it
                        ValidAudience = jwtSettings.Audience,

                        ValidateLifetime = true, //check that the token is not expired
                        ClockSkew = TimeSpan.FromMinutes(jwtSettings.MinutesToExpiration),
                    };
                });

          

            //TODO: Registering EF
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DbConnection")));

           


            //TODO: Configuring Dependency Injection
            services.AddScoped<IBookStoreRepository, BookStoreRepository>();
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddSingleton<JwtSettingsConfiguration>(jwtSettings);
            services.AddScoped<IAuthorizationHandler, UserMustBeBookOwnerRequirementHandler>();
            services.AddScoped<IUserInfoService, UserInfoService>();


            //Enable access Http Context outside the controller
            services.AddHttpContextAccessor();

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //Add Auth Middleware's
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private JwtSettingsConfiguration GetJwtSettings()
        {
            return new JwtSettingsConfiguration()
            {
                Key = Configuration["JwtSettings:Key"],
                Audience = Configuration["JwtSettings:Audience"],
                Issuer = Configuration["JwtSettings:Issuer"],
                MinutesToExpiration = Convert.ToInt32(Configuration["JwtSettings:MinutesToExpiration"])
            };
        }
    }
}
