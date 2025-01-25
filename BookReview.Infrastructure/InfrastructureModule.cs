using BookReview.Core.Repositories;
using BookReview.Core.Services;
using BookReview.Infrastructure.Persistence;
using BookReview.Infrastructure.Persistence.Repositories;
using BookReview.Infrastructure.Services.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookReview.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddRepositories(configuration)
                .AddServices();

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

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
