using FluentValidation;
using Microsoft.Extensions.Logging;
using Minibar.Contracts.Drinks;
using Minibar.Entities.Drinks;

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

        public async Task<Guid> Create(CreateDrinkDTO drinkDTO, CancellationToken cancellationToken)
        {
            // Проверить валидность входных данных (с помощью либы FluentValidation)
            var validationResult = await _validator.ValidateAsync(drinkDTO, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // здесь можно воткнуть валидацию именно бизнес логики

            // Создать сущность Drink
            var drinkId = Guid.NewGuid();

            var drink = new Drink(
                drinkId,
                drinkDTO.Name,
                drinkDTO.Desription,
                drinkDTO.UserId,
                drinkDTO.CategoryId,
                drinkDTO.TagIds);

            // Сохранить её в БД
            await _drinksRepository.CreateAsync(drink, cancellationToken);

            // Залогировать результат (успех или не очень)
            _logger.LogInformation("Drink created with id {drinkId}", drinkId);

            return drinkId;
        }
    }
}
