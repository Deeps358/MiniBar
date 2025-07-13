using FluentValidation;
using Microsoft.Extensions.Logging;
using Minibar.Application.Drinks.Exceptions;
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
                var errors = validationResult.Errors.Select(e => Error.NotValid(
                    e.ErrorCode, e.ErrorMessage, e.PropertyName)).ToArray();

                throw new DrinkValidationException(errors);
            }

            // здесь можно воткнуть валидацию именно бизнес логики

            var nameResult = await _drinksRepository.GetByNameAsync(drinkDTO.Name, cancellationToken);
            if (nameResult != null)
            {
                _logger.LogInformation("Такой напиток уже есть!");
                throw new Exception("Такой напиток уже есть!");
            }

            // Создать сущность Drink

            var drink = new Drink(
                drinkDTO.Name,
                drinkDTO.Description,
                drinkDTO.UserId,
                drinkDTO.CategoryId,
                drinkDTO.TagsIds);

            // Сохранить её в БД
            int drinkId = await _drinksRepository.CreateAsync(drink, cancellationToken);

            // Залогировать результат (успех или не очень)
            _logger.LogInformation("Drink created with id {drinkId}", drinkId);

            return drinkId;
        }
    }
}
