using BookReview.Core.Repositories;
using BookReview.Core.Services;
using BookReview.Infrastructure.Notifications;
using BookReview.Infrastructure.Persistence;
using BookReview.Infrastructure.Persistence.Repositories;
using BookReview.Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookReview.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddRepositories(configuration)
                .AddServices(configuration)
                .AddAuth(configuration);

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookReviewDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("BookReviews")));

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>(); 

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }

        private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthService, AuthService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                      .AddJwtBearer(opt =>
                                      {
                                          opt.TokenValidationParameters = new TokenValidationParameters
                                          {
                                              ValidateIssuer = true,
                                              ValidateAudience = true,
                                              ValidateLifetime = true,
                                              ValidateIssuerSigningKey = true,
                                              ValidIssuer = configuration["Jwt:Issuer"],
                                              ValidAudience = configuration["Jwt:Audience"],
                                              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:key"]))
                                          };
                                      });

            return services;
        }
    }
}
