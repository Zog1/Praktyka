using AutoMapper;
using CarRental.DAL;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.DAL.Repositories;
using CarRental.Services;
using CarRental.Services.Interfaces;
using CarRental.Services.Mapper;
using CarRental.Services.Models.Car;
using CarRental.Services.Models.Email_Templates;
using CarRental.Services.Models.Location;
using CarRental.Services.Models.Reservation;
using CarRental.Services.Models.User;
using CarRental.Services.Services;
using CarRental.Services.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace CarRental.API.StartupExtensions
{
    public static class StartupExtension
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString)
            .EnableSensitiveDataLogging());
        }

        public static IServiceCollection AddMappingServices(this IServiceCollection services)
        {
            return services
                .AddSingleton<Profile, ReservationProfile>()
                .AddSingleton<Profile, UserProfile>()
                .AddSingleton<Profile, CarProfile>()
                .AddSingleton<Profile, LocationProfile>()
                .AddSingleton<Profile, DefectProfile>()
                .AddSingleton<IConfigurationProvider, AutoMapperConfiguration>(p =>
                    new AutoMapperConfiguration(p.GetServices<Profile>()))
                .AddSingleton<IMapper, Mapper>();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IEmailServices, EmailService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IDefectsService, DefectService>();
            services.AddScoped<ITermService, TermService>();
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
            services.AddTransient<ApplicationDbContextDataSeed>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IRefreshRepository, RefreshRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IDefectRepository, DefectRepository>();
            return services;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<ReservationCreateDto>, ReservationCreateDtoValidator>();
            services.AddTransient<IValidator<ReservationUpdateDto>, ReservationUpdateDtoValidator>();
            services.AddTransient<IValidator<CreateUserDto>, CreateUserDtoValidator>();
            services.AddTransient<IValidator<UpdateUserPasswordDto>, UpdateUserPasswordValidator>();
            services.AddTransient<IValidator<CarDto>, CarDtoValidator>();
            services.AddTransient<IValidator<CarCreateDto>, CarCreateDtoValidator>();
            services.AddTransient<IValidator<LocationCreateDto>, LocationCreateDtoValidator>();
            services.AddTransient<IValidator<UserLoginDto>, UserLoginValidatorDto>();
            return services;
        }
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "Roles",
                        ValidateIssuer = true,
                        ValidIssuer = TokenOptions.ISSUER,
                        ValidAudience = TokenOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = TokenOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });
            return services;
        }
    }
}
