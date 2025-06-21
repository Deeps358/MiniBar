using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibar.Entities.Drinks
{
    // анемичная модель
    internal class Drink
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public required Guid UserId { get; set; }

        public required Guid CategoryId { get; set; }

        public List<Guid> Tags { get; set; } = [];

        public required DateTime CreatedAt { get; set; }
    }
}
