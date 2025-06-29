namespace Minibar.Contracts.Drinks
{
    // дто для передачи в контроллер
    public record CreateDrinkDTO(string Name, string Description, Guid UserId, Guid CategoryId, Guid[] TagsIds);
}
