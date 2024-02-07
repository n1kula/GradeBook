using Gradebook.Domain.Abstractions;
using Gradebook.Domain.Exceptions;
using MediatR;

namespace Gradebook.Application.Commands.Students.RemoveStudent
{
    internal class RemoveStudentCommandHandler : IRequestHandler<RemoveStudentCommand>
    {
        private IStudentRepository _studentRepository;
        private IUnitOfWork _unitOfWork;

        public RemoveStudentCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(RemoveStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.Id, cancellationToken);
            if (student == null)
            {
                throw new StudentNotFoundException(request.Id);
            }

            _studentRepository.Delete(student);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
