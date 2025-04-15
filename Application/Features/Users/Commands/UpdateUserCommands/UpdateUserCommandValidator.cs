using FluentValidation;

namespace Application.Features.Users.Commands.UpdateUserCommands
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(p => p.FirstName)
                 .NotEmpty().WithMessage("{PropertyName} is required.")
                 .NotNull()
                 .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.BirthDate)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .LessThan(DateTime.Now).WithMessage("{PropertyName} can't be greater than the current date.");

            RuleFor(p => p.ContactNumber)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Matches(@"^\d{3}-\d{3}-\d{4}$").WithMessage("{PropertyName} must have the (000-000-0000) format.")
                .MaximumLength(12).WithMessage("{PropertyName} can't exceed the limit of {MaxLength}.");

        }
    }
}
