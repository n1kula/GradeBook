using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gradebook.Domain.Abstractions;
using Gradebook.Domain.Exceptions;
using MediatR;

namespace Gradebook.Application.Commands.Students.UpdateStudent
{
    internal class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
    {
        private IStudentRepository _studentRepository;
        private IUnitOfWork _unitOfWork;

        public UpdateStudentCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);
            if (student == null)
            {
                throw new StudentNotFoundException(request.Id);
            }

            student.FirstName = request.FirstName;
            student.LastName = request.LastName;
            student.Email = request.Email;
            student.DOB = request.DOB;
            student.YearEnrolled = request.YearEnrolled;

            _studentRepository.Update(student);
            await _unitOfWork.SaveChangesAsync();
        }

        Task<Unit> IRequestHandler<UpdateStudentCommand, Unit>.Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
