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

            var getUserByUsername = await _usersRepository.GetByEmailAsync(userDTO.Email, cancellationToken);
            if (getUserByUsername != null)
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
    }
}
