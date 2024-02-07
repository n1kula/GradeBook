using Gradebook.Application.Dtos;
using Gradebook.Domain.Abstractions;
using MediatR;

namespace Gradebook.Application.Queries.Students.GetStudents
{
    internal class GetStudentsQueryHandler : IRequestHandler<GetStudentsQuery, IEnumerable<StudentDto>>
    {
        private readonly IStudentReadOnlyRepository _studentReadOnlyRepository;
        public GetStudentsQueryHandler(IStudentReadOnlyRepository studentReadOnlyRepository)
        {
            _studentReadOnlyRepository = studentReadOnlyRepository;
        }
        public async  Task<IEnumerable<StudentDto>> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentReadOnlyRepository.GetAllAsync(cancellationToken);

            var studentsDto = students.Select(x => new StudentDto 
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Age = DateTime.Now.Year - x.DOB.ToDateTime(TimeOnly.Parse("00:00")).Year,
                YearEnrolled = x.YearEnrolled
            });
            return studentsDto;
        }
    }
}
