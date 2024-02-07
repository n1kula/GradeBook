using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Gradebook.Application
{
    public static class Extensions
    {
        public static object AddApplication(this IServiceCollection services)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(executingAssembly);
            services.AddAutoMapper(executingAssembly);
           
            return services;
        }
    }
}
