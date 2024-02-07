using FluentValidation;

namespace Gradebook.Application.Commands.Students.AddStudent
{
    public class AddStudentCommandValidation : AbstractValidator<AddStudentCommand>
    {
        public AddStudentCommandValidation()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First nama is required")
                .MaximumLength(50).WithMessage("First name can't be longer than 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("LAst name can't be longer than 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email nama is required")
                .MaximumLength(100).WithMessage("Email can't be longer than 50 characters.")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.DOB)
               .NotEmpty().WithMessage("DOB is required");

            RuleFor(x => x.YearEnrolled)
               .NotEmpty().WithMessage("Year enrolled is required");
        }
    }
}
