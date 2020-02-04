using CleanArchitecture.Application.Common.Validation;
using FluentValidation;

namespace CleanArchitecture.Application.TodoItems.Commands.CreateTodoItem
{
    public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
    {
        
        public CreateTodoItemCommandValidator()
        {            
            RuleFor(v => v.Title)
                .NotEmpty()
                    .WithErrorCode(ValidationErrorCodes.TODO_ITEM_TITLE_REQUIRED)
                    .WithMessage("Title is required.")
                .MaximumLength(10)
                    .WithErrorCode(ValidationErrorCodes.TODO_ITEM_TITLE_TOO_LONG)
                    .WithMessage("Title must not exceed 200 characters.");
        }
    }
}
