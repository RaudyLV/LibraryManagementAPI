using FluentValidation;

namespace Application.Features.Users.Commands.RegisterCommand
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>    
    {
        public RegisterCommandValidator()
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


            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .EmailAddress().WithMessage("{PropertyName} is not a valid email address.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(6).WithMessage("{PropertyName} must be at least 6 characters.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(p => p.ConfirmPassword)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Equal(p => p.Password).WithMessage("Passwords do not match.");
        }
    }
}
