using FluentValidation;
using Minibar.Contracts.Categories;

namespace Minibar.Application.Categories
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryDTO>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Категория не должна быть пустой!")
                .MaximumLength(20).WithMessage("Слишком длинное название!");
        }
    }
}
