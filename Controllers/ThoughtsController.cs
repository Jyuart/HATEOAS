using System;
using System.Collections.Generic;
using System.Linq;
using HateoasDemo.BL;
using Microsoft.AspNetCore.Mvc;

namespace HateoasDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ThoughtsController : ControllerBase
    {
        private IEnumerable<ThoughtDto> _thoughts;

        public ThoughtsController()
        {
            SeedThoughts();
        }

        [HttpPost]
        public IActionResult Post()
        {
            var thought = new ThoughtDto
            {
                Id = 1,
                Name = "New thought"
            };
            return CreatedAtRoute(nameof(Get), new { thoughtId = thought.Id }, thought);
        }

        [HttpGet("{thoughtId}", Name = nameof(Get))]
        public IActionResult Get(int thoughtId)
        {
            ThoughtDto thought = _thoughts.First(x => x.Id == thoughtId);

            return Ok(thought);
        }

        [HttpGet(Name = nameof(GetAll))]
        public IActionResult GetAll()
        {
            var thoughts = new ThoughtListDto
            {
                Thoughts = _thoughts
            };

            return Ok(thoughts);
        }

        [HttpGet("empty", Name = nameof(GetAllEmpty))]
        public IActionResult GetAllEmpty()
        {
            var thoughts = new ThoughtListDto
            {
                Thoughts = new List<ThoughtDto>()
            };

            return Ok(thoughts);
        }

        private void SeedThoughts()
        {
            _thoughts = new List<ThoughtDto>
            {
                new()
                {
                    Id = 1,
                    Name = "Have a rest",
                    Description = "Don't overtime",
                    OccurredOn = new DateTime(2021, 1, 1)
                },

                new ()
                {
                    Id = 2,
                    Name = "Tell about HATEAOS",
                    Description = "Prepare a tutorial and provide a demo code",
                    OccurredOn = new DateTime(2021, 4, 15)
                }
            };
        }
    }
}