using BookReview.Application.Commads.AuthorCommands.Create;
using BookReview.Application.Services;
using BookReview.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookReview.Application
{
    public static class Module
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHandlers()
                .AddServices();
            
            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(config =>
                config.RegisterServicesFromAssemblyContaining<CreateAuthorCommand>());

           // services.AddTransient<IPipelineBehavior<ForgotPassowrdCommand, ResultViewModel>, ForgotPassowrdBehavior>();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
