using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.DTOs;

namespace ToDo.Validation
{
    public class TodoValidator : AbstractValidator<TodoDTO>
    {
        public TodoValidator()
        {
            RuleFor(todo => todo.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(todo => todo.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(todo => todo.StartDate).LessThan(todo => todo.EndDate)
                .WithMessage("Start date must be before end date.");
        }
    }
}
