namespace Minibar.Entities.Drinks
{
    // анемичная модель
    public class Drink
    {
        public Drink(
            string name,
            string description,
            int userId,
            int categoryId,
            IEnumerable<int> tags)
        {
            Name = name;
            Description = description;
            UserId = userId;
            CategoryId = categoryId;
            Tags = tags.ToList();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public int UserId { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<int> Tags { get; set; } = Enumerable.Empty<int>();

        public DateTime CreatedAt { get; set; }
    }
}
