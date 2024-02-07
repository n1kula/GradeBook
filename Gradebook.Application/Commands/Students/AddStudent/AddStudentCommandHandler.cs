using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
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
        private IMapper _mapper;
        private IValidator _validator;

        public AddStudentCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork, IMapper mapper, IValidator<AddStudentCommandValidation> validator)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<StudentDto> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            ValidationResult result = _validator.Validate((IValidationContext)request);
            if (!result.IsValid)
            {
                var errorList = result.Errors.Select(x => x.ErrorMessage);
                throw new ValidationException($"Invalid command, reasons: {string.Join(",", errorList.ToArray())}");
            }
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

            var studentDto = _mapper.Map<StudentDto>(newStudent);
            return studentDto;
        }
    }
}
