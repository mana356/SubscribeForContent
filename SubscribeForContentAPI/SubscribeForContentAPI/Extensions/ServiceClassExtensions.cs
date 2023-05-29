using SFC_DataAccess.Repository;
using SFC_DataAccess.Repository.Contracts;
using SubscribeForContentAPI.Models;
using SubscribeForContentAPI.Services;
using SubscribeForContentAPI.Services.Contracts;

namespace SubscribeForContentAPI.Extensions
{
    public static class ServiceClassExtensions
    {
        public static IServiceCollection InitializeAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IFileContentRepository, FileContentRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
            services.AddSingleton<IBlobStorage, BlobStorageService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
    }
}
