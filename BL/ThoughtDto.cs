using System;

namespace HateoasDemo.BL
{
    public record ThoughtDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DateTime OccurredOn { get; init; }
    }
}
