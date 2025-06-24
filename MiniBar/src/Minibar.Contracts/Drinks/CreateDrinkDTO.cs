namespace Minibar.Contracts.Drinks
{
    // дто для передачи в контроллер
    public record CreateDrinkDTO(string Name, string Desription, Guid UserId, Guid CategoryId, Guid[] TagIds);
}
