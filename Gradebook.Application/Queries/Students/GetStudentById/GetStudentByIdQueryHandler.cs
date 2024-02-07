using AutoMapper;
using Gradebook.Application.Dtos;
using Gradebook.Domain.Abstractions;
using MediatR;

namespace Gradebook.Application.Queries.Students.GetStudentById
{
    internal class GetStudentByEmailQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public GetStudentByEmailQueryHandler(IStudentRepository studentRepository, IMapper  mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
        public  async Task<StudentDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);

            var studentDto = _mapper.Map<StudentDto>(student);
            return studentDto;
        }
    }
}
