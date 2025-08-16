using System.Security.Claims;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Minibar.Application.Drinks;
using Minibar.Application.Extensions;
using Minibar.Application.Users.Failures;
using Minibar.Application.Users.Failures.Exceptions;
using Minibar.Contracts.Users;
using Minibar.Entities.Users;
using Shared;

namespace Minibar.Application.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IValidator<CreateUserDTO> _validator;
        private readonly ILogger<DrinksService> _logger;

        public UsersService(
            IUsersRepository usersRepository,
            ILogger<DrinksService> logger,
            IHttpContextAccessor httpContextAccessor,
            IValidator<CreateUserDTO> validator)
        {
            _usersRepository = usersRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _validator = validator;
        }

        public async Task<Result<int, Failure>> CreateAsync(CreateUserDTO userDTO, CancellationToken cancellationToken)
        {
            // валидация
            var validationResult = await _validator.ValidateAsync(userDTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new UserValidationException(validationResult.ToErrors()); // Extension метод
            }

            // проверить что такой почты ещё нет
            var getUser = await _usersRepository.GetByUserNameAsync(userDTO.UserName, cancellationToken);
            if (getUser != null)
            {
                throw new UserAlreadyExistsException([Errors.Users.UserAlreadyExist()]);
            }

            // TODO: проверить что такая роль есть

            // обработка пароля
            var hasher = new PasswordHasher<string>();
            string hashedPassword = hasher.HashPassword(null, userDTO.Password);

            // создание сущности
            var user = new User(
                userDTO.UserName,
                hashedPassword,
                userDTO.Email,
                userDTO.RoleId);

            // сохраняем в БД
            int userId = await _usersRepository.CreateAsync(user, cancellationToken);

            return userId;
        }

        public async Task<Result<int, Failure>> Login(LoginUserDTO loginUserDTO, CancellationToken cancellationToken)
        {
            var getUser = await _usersRepository.GetByUserNameAsync(loginUserDTO.UserName, cancellationToken);
            if (getUser == null)
            {
                throw new Exception("Пользователя с таким логином нет!");
            }

            var hasher = new PasswordHasher<string>();
            var passwordChecker = hasher.VerifyHashedPassword(null, getUser.PasswordHash, loginUserDTO.Password);
            if (passwordChecker == PasswordVerificationResult.Success)
            {
                string userRoleName = await _usersRepository.GetRoleByIdAsync(getUser.RoleId, cancellationToken);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, getUser.UserName),
                    new Claim(ClaimTypes.Role, userRoleName), // Например, "Admin" или "User"
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await _httpContextAccessor.HttpContext?.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    new AuthenticationProperties
                    {
                        IsPersistent = true // Куки сохраняются после закрытия браузера
                    }
                );

                return getUser.Id;
            }
            else if (passwordChecker == PasswordVerificationResult.SuccessRehashNeeded)
            {
                throw new Exception("Вам надо обновить пароль!");
            }
            else
            {
                throw new Exception("Неправильный пароль!");
            }
        }

        public async Task<Result<string, Failure>> Logout(CancellationToken cancellationToken)
        {
            await _httpContextAccessor.HttpContext?.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return "Вы успешно вышли из системы!";
        }

        public async Task<Result<string, Failure>> WhoIAm(CancellationToken cancellationToken)
        {
            string myName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
            return $"Вы залогинены как {myName}";
        }
    }
}
