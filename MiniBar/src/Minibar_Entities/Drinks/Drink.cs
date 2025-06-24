using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibar.Entities.Drinks
{
    // анемичная модель
    public class Drink
    {
        public Drink(
            Guid id,
            string name,
            string description,
            Guid userId,
            Guid categoryId,
            IEnumerable<Guid> tags)
        {
            Id = id;
            Name = name;
            Description = description;
            UserId = userId;
            CategoryId = categoryId;
            Tags = tags.ToList();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public Guid CategoryId { get; set; }

        public List<Guid> Tags { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
