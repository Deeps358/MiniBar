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
            string name,
            string description,
            Guid userId,
            Guid categoryId,
            IEnumerable<Guid> tags)
        {
            this.name = name;
            this.description = description;
            this.userId = userId;
            this.categoryId = categoryId;
            this.tags = tags.ToList();
        }

        public int id { get; set; }

        public string name { get; set; }

        public string description { get; set; } = string.Empty;

        public Guid userId { get; set; }

        public Guid categoryId { get; set; }

        public IEnumerable<Guid> tags { get; set; } = Enumerable.Empty<Guid>();

        public DateTime createdAt { get; set; }
    }
}
