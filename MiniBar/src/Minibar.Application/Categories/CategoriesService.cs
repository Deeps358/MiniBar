using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Minibar.Application.Categories.Failures;
using Minibar.Application.Extensions;
using Minibar.Contracts.Categories;
using Minibar.Entities.Categories;
using Shared;

namespace Minibar.Application.Categories
{
    public class CategoriesService : ICategoriesService
    {
        private ICategoriesRepository _categoriesRepository;
        private IValidator<CreateCategoryDTO> _validator;
        private ILogger<CategoriesService> _logger;

        public CategoriesService(
            ICategoriesRepository categoriesRepository,
            IValidator<CreateCategoryDTO> validator,
            ILogger<CategoriesService> logger)
        {
            _categoriesRepository = categoriesRepository;
            _validator = validator;
            _logger = logger;
        }

        public async Task<Result<Category?, Failure>> GetAsync(int categoryId, CancellationToken cancellationToken)
        {
            return await _categoriesRepository.GetAsync(categoryId, cancellationToken);
        }

        public async Task<Result<Category?, Failure>> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _categoriesRepository.GetByNameAsync(name, cancellationToken);
        }

        public async Task<Result<Category[]?, Failure>> GetFewAsync(int[] categoryIds, CancellationToken cancellationToken)
        {
            return await _categoriesRepository.GetFewAsync(categoryIds, cancellationToken);
        }

        public async Task<Result<Category[]?, Failure>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _categoriesRepository.GetAllAsync(cancellationToken);
        }

        public async Task<Result<int, Failure>> CreateAsync(CreateCategoryDTO dto, CancellationToken cancellationToken)
        {
            // Проверить валидность входных данных (с помощью либы FluentValidation)
            var validationResult = await _validator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
            {
                return validationResult.ToErrors(); // и ошибки вернём и код дальше не пойдёт
            }

            var category = await _categoriesRepository.GetByNameAsync(dto.name, cancellationToken);
            if (category != null)
            {
                _logger.LogInformation($"Попытка добавить существующую категорию с названием {dto.name}!");
                return Errors.Categories.CategoryAlreadyExist().ToFailure();
            }

            // Создать сущность Category
            var newCategory = new Category { Name = dto.name };

            int categoryId = await _categoriesRepository.CreateAsync(newCategory, cancellationToken);

            _logger.LogInformation($"Добавлена категория с id {categoryId}");

            return categoryId;
        }

        public async Task<Result<int, Failure>> UpdateAsync(CreateCategoryDTO dto, CancellationToken cancellationToken) => throw new NotImplementedException();

        public async Task<Result<string, Failure>> DeleteAsync(int id, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}
