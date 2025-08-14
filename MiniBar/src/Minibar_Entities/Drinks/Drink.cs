namespace Minibar.Entities.Drinks
{
    // анемичная модель
    public class Drink
    {
        public Drink(
            string name,
            string description,
            string picturePath,
            int userId,
            int categoryId,
            IEnumerable<int> tags)
        {
            Name = name;
            Description = description;
            PicturePath = picturePath;
            UserId = userId;
            CategoryId = categoryId;
            if (tags != null)
            {
                Tags = tags.ToArray();
            }

            CreatedAt = DateTime.Now.ToUniversalTime();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; } = string.Empty;

        public string? PicturePath { get; set; }

        public int UserId { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<int> Tags { get; set; } = Enumerable.Empty<int>();

        public DateTime CreatedAt { get; set; }
    }
}
