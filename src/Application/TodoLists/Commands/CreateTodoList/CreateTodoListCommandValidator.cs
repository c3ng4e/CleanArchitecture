using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.TodoLists.Commands.CreateTodoList
{
    public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoListCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .NotEmpty().WithErrorCode(ValidationErrorCodes.TODO_LIST_TITLE_REQUIRED).WithMessage("Title is required.")
                .MaximumLength(200).WithErrorCode(ValidationErrorCodes.TODO_LIST_TITLE_TOO_LONG).WithMessage("Title must not exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithErrorCode(ValidationErrorCodes.TODO_LIST_TITLE_NOT_UNIQUE).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return await _context.TodoLists
                .AllAsync(l => l.Title != title);
        }
    }
}
