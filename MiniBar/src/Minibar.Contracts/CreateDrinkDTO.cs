namespace Minibar.Contracts
{
    // дто для передачи в контроллер
    public record CreateDrinkDTO(string Name, string Desription, Guid UserId, Guid[] TagIds);
}
