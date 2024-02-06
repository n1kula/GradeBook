using Gradebook.Application.Dtos;
using MediatR;

namespace Gradebook.Application.Queries.Students.GetStudentById
{
    public record GetStudentByEmailQuery(int Id) : IRequest<StudentDto>
    {
    }
}
