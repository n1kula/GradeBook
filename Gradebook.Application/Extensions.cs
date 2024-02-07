using System.Reflection;
using FluentValidation;
using Gradebook.Application.Commands.Students.AddStudent;
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
            services.AddScoped<IValidator<AddStudentCommand>, AddStudentCommandValidation>();
           
            return services;
        }
    }
}
