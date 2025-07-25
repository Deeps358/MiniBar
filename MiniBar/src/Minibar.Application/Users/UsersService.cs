using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Minibar.Application.Drinks;
using Minibar.Contracts.Users;
using Minibar.Entities.Users;

namespace Minibar.Application.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<DrinksService> _logger;

        public UsersService(
            IUsersRepository usersRepository,
            ILogger<DrinksService> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _usersRepository = usersRepository;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> Create(CreateUserDTO userDTO, CancellationToken cancellationToken)
        {
            // валидация (попозже)

            // проверить что такой почты ещё нет

            var getUser = await _usersRepository.GetByUserNameAsync(userDTO.UserName, cancellationToken);
            if (getUser != null)
            {
                throw new Exception("Пользователь с таким мылом уже есть!");
            }

            // обработка пароля

            var hasher = new PasswordHasher<string>();
            string hashedPassword = hasher.HashPassword(null, userDTO.Password);

            // создание сущности

            var user = new User(
                userDTO.UserName,
                hashedPassword,
                userDTO.Email,
                userDTO.RoleId);

            int userId = await _usersRepository.CreateAsync(user, cancellationToken);

            return userId;
        }

        public async Task<int> Login(LoginUserDTO loginUserDTO, CancellationToken cancellationToken)
        {
            var getUser = await _usersRepository.GetByUserNameAsync(loginUserDTO.UserName, cancellationToken);
            if (getUser == null)
            {
                throw new Exception("Пользователя с таким мылом нет!");
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

        public async Task<string> Logout(CancellationToken cancellationToken)
        {
            await _httpContextAccessor.HttpContext?.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return "Вы успешно вышли из системы!";
        }

        public async Task<string> WhoIAm(CancellationToken cancellationToken)
        {
            string myName = _httpContextAccessor.HttpContext?.User.Identity?.Name;
            return $"Вы залогинены как {myName}";
        }
    }
}
