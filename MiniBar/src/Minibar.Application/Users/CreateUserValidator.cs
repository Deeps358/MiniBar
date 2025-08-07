using FluentValidation;
using Minibar.Contracts.Users;

namespace Minibar.Application.Users
{
    public class CreateUserValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Логин должен быть заполнен!")
                .MaximumLength(20).WithMessage("Пустой или длинный (20) логин");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Почта должна быть заполнена!")
                .MaximumLength(50).WithMessage("Пустая или длинная (50) почта");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Пароль не должен быть пустым!")
                .MinimumLength(8).WithMessage("Ваш пароль менее 8 символов!")
                .MaximumLength(20).WithMessage("Пустой или длинный (20) пароль");
        }
    }
}
