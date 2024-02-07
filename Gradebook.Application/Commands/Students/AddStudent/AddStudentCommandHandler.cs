using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gradebook.Application.Dtos;
using Gradebook.Domain.Abstractions;
using Gradebook.Domain.Entities;
using Gradebook.Domain.Exceptions;
using MediatR;

namespace Gradebook.Application.Commands.Students.AddStudent
{
    internal class AddStudentCommandHandler : IRequestHandler<AddStudentCommand, StudentDto>
    {
        private IStudentRepository _studentRepository;
        private IUnitOfWork _unitOfWork;

        public AddStudentCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<StudentDto> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            bool isAlreadyExist = await _studentRepository.IsAlreadyExistAsync(request.Email, cancellationToken);
            if (isAlreadyExist)
            {
                throw new StudentAlreadyExistException(request.Email);
            }

            var newStudent = new Student()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                DOB = request.DOB,
                YearEnrolled = request.YearEnrolled
            };

            _studentRepository.Add(newStudent);
            await _unitOfWork.SaveChangesAsync();

            var studentDto = new StudentDto()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Age = DateTime.Now.Year - newStudent.DOB.ToDateTime(TimeOnly.Parse("00:00")).Year,,
                YearEnrolled = request.YearEnrolled
            };
            return studentDto;
        }
    }
}
