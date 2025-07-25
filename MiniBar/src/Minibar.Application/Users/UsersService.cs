using FluentValidation;
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
        private readonly ILogger<DrinksService> _logger;

        public UsersService(
            IUsersRepository usersRepository,
            ILogger<DrinksService> logger)
        {
            _usersRepository = usersRepository;
            _logger = logger;
        }

        public async Task<int> Create(CreateUserDTO userDTO, CancellationToken cancellationToken)
        {
            // валидация (попозже)

            // проверить что такой почты ещё нет

            var getUserByEmail = await _usersRepository.GetByEmailAsync(userDTO.Email, cancellationToken);
            if (getUserByEmail != null)
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
                userDTO.Email);

            int userId = await _usersRepository.CreateAsync(user, cancellationToken);

            return userId;
        }

        public async Task<int> Login(LoginUserDTO loginUserDTO, CancellationToken cancellationToken)
        {
            var getUserByEmail = await _usersRepository.GetByEmailAsync(loginUserDTO.Email, cancellationToken);
            if (getUserByEmail == null)
            {
                throw new Exception("Пользователя с таким мылом нет!");
            }

            var hasher = new PasswordHasher<string>();
            var passwordChecker = hasher.VerifyHashedPassword(null, getUserByEmail.PasswordHash, loginUserDTO.Password);
            if (passwordChecker == PasswordVerificationResult.Success)
            {
                return getUserByEmail.Id;
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
    }
}
