using Application.Interfaces;
using Application.Wrrappers;
using Domain.Settings;
using identity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Persistence.Contexts;
using Persistence.Repositories;
using Persistence.Services;
using System.Security.Claims;
using System.Text;

namespace Persistence
{
    public static class ServiceExtensions
    {
        public static void AddPersistenceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            #region Repositories 
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            #endregion

            #region Services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUserService, UserServices>();
            services.AddTransient<ITransaction, DbContextTransactionWrapper>();
            services.AddTransient<ILoanService, LoanService>();
            services.AddTransient<IBookService, BookService>();
            #endregion

            var key = configuration["JWTSettings:Key"]
                                     ?? throw new ArgumentNullException("JWTSettings:Key is missing in configuration");

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(op =>
            {
                op.RequireHttpsMetadata = false;
                op.SaveToken = false;
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTSettings:Issuer"],
                    ValidAudience = configuration["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    RoleClaimType = ClaimTypes.Role
                };


                op.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = async ct =>
                    {
                        Console.WriteLine(ct.Response.ToString());
                        if (ct.Response.HasStarted) return;
                        ct.NoResult();
                        ct.Response.StatusCode = 500;
                        ct.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>(ct.Exception.ToString()));
                        Console.WriteLine(result.ToString());
                        Console.WriteLine(ct.Response.ToString());
                        await ct.Response.WriteAsync(result);
                    },

                    OnChallenge = async ct =>
                    {
                        if (ct.Response.HasStarted) return;
                        ct.HandleResponse();
                        ct.Response.StatusCode = 401;
                        ct.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized."));
                        await ct.Response.WriteAsync(result);
                    },

                    OnForbidden = async ct =>
                    {
                        if (ct.Response.HasStarted) return;
                        ct.Response.StatusCode = 403;
                        ct.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized for this section."));
                        await ct.Response.WriteAsync(result);
                    }
                };
            });

        }
    }
}
