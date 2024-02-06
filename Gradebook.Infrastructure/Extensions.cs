using Gradebook.Domain.Abstractions;
using Gradebook.Infrastructure.Context;
using Gradebook.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gradebook.Infrastructure
{
    public static class Extensions
    {
        public static object AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddDbContext<GradebookDbContext>(ctx => ctx.UseSqlServer(configuration.GetConnectionString("GradebookCS")));
            return services;
        }
    }
}
