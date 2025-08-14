using FluentValidation;
using Microsoft.Extensions.Logging;
using Minibar.Application.Drinks.Failures;
using Minibar.Application.Drinks.Failures.Exceptions;
using Minibar.Application.Extensions;
using Minibar.Contracts.Drinks;
using Minibar.Entities.Drinks;
using Shared;

namespace Minibar.Application.Drinks
{
    public class DrinksService : IDrinksService
    {
        private readonly IDrinksRepository _drinksRepository;
        private readonly IValidator<CreateDrinkDTO> _validator;
        private readonly ILogger<DrinksService> _logger;

        public DrinksService(
            IDrinksRepository drinksRepository,
            IValidator<CreateDrinkDTO> validator,
            ILogger<DrinksService> logger)
        {
            _drinksRepository = drinksRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<int> Create(CreateDrinkDTO drinkDTO, CancellationToken cancellationToken)
        {
            // Проверить валидность входных данных (с помощью либы FluentValidation)
            var validationResult = await _validator.ValidateAsync(drinkDTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new DrinkValidationException(validationResult.ToErrors()); // Extension метод
            }

            // здесь валидация именно бизнес логики
            var nameResult = await _drinksRepository.GetByNameAsync(drinkDTO.Name, cancellationToken);
            if (nameResult != null)
            {
                _logger.LogInformation("Попытка добавить существующий напиток!");
                throw new DrinkAlreadyExistException([Errors.Drinks.DrinkAlreadyExist()]);
            }

            // Создать сущность Drink
            var drink = new Drink(
                drinkDTO.Name,
                drinkDTO.Description,
                drinkDTO.PicturePath,
                drinkDTO.UserId,
                drinkDTO.CategoryId,
                drinkDTO.TagsIds);

            // Сохранить её в БД
            int drinkId = await _drinksRepository.CreateAsync(drink, cancellationToken);

            // Залогировать результат (успех или не очень)
            _logger.LogInformation("Drink created with id {drinkId}", drinkId);

            return drinkId;
        }

        public async Task<Drink[]?> GetAll(CancellationToken cancellationToken)
        {
            return await _drinksRepository.GetAllAsync(cancellationToken);
        }
    }
}
