using System.Collections.Generic;
using System.Linq;

namespace HateoasDemo.BL
{
    public record ThoughtListDto
    {
        public IEnumerable<ThoughtDto> Thoughts { get; init; }
        public int Total => Thoughts.Count();
    }
}
