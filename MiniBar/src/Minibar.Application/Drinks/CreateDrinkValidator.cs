using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Minibar.Contracts.Drinks;

namespace Minibar.Application.Drinks
{
    public class CreateDrinkValidator : AbstractValidator<CreateDrinkDTO>
    {
        public CreateDrinkValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).WithMessage("Пустой или длинный (50) заголовок");
            RuleFor(x => x.Desription).NotEmpty().MaximumLength(500).WithMessage("Пустое или длинное (500) описание");
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
