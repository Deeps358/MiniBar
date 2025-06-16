using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibar.Entities.Tags
{
    internal class Tag
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
