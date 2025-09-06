using Microsoft.AspNetCore.Http;

namespace Minibar.Contracts.Drinks
{
    // дто для передачи в контроллер
    public record CreateDrinkDTO(
        string Name,
        string? Description,
        IFormFile? Photo,
        int UserId,
        int CategoryId,
        int[]? TagsIds);
}
