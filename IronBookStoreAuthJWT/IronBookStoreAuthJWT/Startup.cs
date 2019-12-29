using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IronBookStoreAuthJWT.Authorization;
using IronBookStoreAuthJWT.Core.Entities;
using IronBookStoreAuthJWT.Core.Services;
using IronBookStoreAuthJWT.Data;
using IronBookStoreAuthJWT.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace IronBookStoreAuthJWT
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
            //TODO: Declare policies
            services.AddAuthorization(authOptions =>
            {
                authOptions.AddPolicy("UserMustBeGeneralManagerOrUpper", policyBuilder =>
                {
                    policyBuilder
                    .RequireRole("Administrator", "GeneralManager"); //User must be Administrator AND GeneralManager
                });

                authOptions.AddPolicy(
                   "UserMustBeBookOwner",
                   policyBuilder =>
                   {
                       policyBuilder
                       .RequireRole("Administrator")
                       .AddRequirements(
                           new UserMustBeBookOwnerRequirement("Clean Code"));
                   });
            });

            var jwtSettings = GetJwtSettings();
            services.AddControllers();

            //Configure Json Web tokens
            /*
             * Register Jwt as the Authentication service
             * Register our Authentication Schema --> We use JWT: JwtBearerDefaults.AuthenticationScheme
             * Configurer our Authentication Schema --> We specify needed parameters for consider a token valid
             */
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        /*
                         * TODO: User.Identity.Name & JWT & JwtRegisteredClaimNames.Sub
                         * With this line we map: JwtRegisteredClaimNames.Sub with System.Security.Claims.ClaimTypes.NameIdentifier
                         * After that we can use: User.Identity.Name in the controller, otherwise User.Identity.Name = null.
                         * Another alternative could be to use: new Claim(ClaimTypes.Name,, user.UserId.ToString()) when 
                         * we define the claims when we generate the token in: IronBookStoreAuthJWT.Core.Services.SecurityService.
                         * Another alternative User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value instead of
                         * User.Identity.Name.
                         */
                        NameClaimType = System.Security.Claims.ClaimTypes.NameIdentifier,

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

            //Add authentication middleware
            app.UseAuthentication();

            /*
             * app.UseAuthorization(); is generated automatically for the FW
             * The template-generated app doesn't use authorization "app.UseAuthorization" is included to ensure it's added 
             * in the correct order should the app add authorization. UseRouting, UseAuthentication, UseAuthorization, and 
             * UseEndpoints must be called in this order.
             */
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
