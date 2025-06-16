using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibar.Entities.Ratings
{
    internal class Rating
    {
        public Guid Id { get; set; }
        public float Score { get; set; }
        public List<Guid> UserIds { get; set; }
        public Guid EntityId { get; set; }
    }
}
