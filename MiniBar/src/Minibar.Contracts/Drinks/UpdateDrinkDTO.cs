namespace Minibar.Contracts.Drinks
{
    public record UpdateDrinkDTO(string Name, string Desription, Guid[] TagIds);
}
