using FluentValidation;

namespace Application.Features.Books.Commands.CreateBookCommands
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(200).WithMessage("{PropertyName} can't exceed the limit of {MaxLength}");
            RuleFor(b => b.Author)
                .NotEmpty().WithMessage("The author name is required.")
                .MaximumLength(80).WithMessage("{PropertyName can't exceed the limit of {MaxLength}");

            RuleFor(b => b.Description)
                .MaximumLength(200).WithMessage("{PropertyName can't exceed the limit of {MaxLength}");

            RuleFor(b => b.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} has to be greater than 0");

            RuleFor(b=> b.Genre)
           .NotEmpty().WithMessage("{PropertyName} is required")
           .MaximumLength(50).WithMessage("{PropertyName can't exceed the limit of {MaxLength}");
        }
    }
}
