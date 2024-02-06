using Gradebook.Application.Dtos;
using Gradebook.Domain.Abstractions;
using MediatR;

namespace Gradebook.Application.Queries.Students.GetStudentById
{
    internal class GetStudentByEmailQueryHandler : IRequestHandler<GetStudentByEmailQuery, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        public GetStudentByEmailQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public  async Task<StudentDto> Handle(GetStudentByEmailQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);

            var studentDto = new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Age = DateTime.Now.Year - student.DOB.ToDateTime(TimeOnly.Parse("00:00")).Year,
                YearEnrolled = student.YearEnrolled
            };
            return studentDto;
        }
    }
}
