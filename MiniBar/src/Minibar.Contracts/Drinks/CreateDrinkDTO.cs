﻿namespace Minibar.Contracts.Drinks
{
    // дто для передачи в контроллер
    public record CreateDrinkDTO(string Name, string Description, int UserId, int CategoryId, int[] TagsIds);
}
