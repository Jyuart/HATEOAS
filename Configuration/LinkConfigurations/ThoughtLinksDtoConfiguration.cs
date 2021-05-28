using HateoasDemo.BL;
using HateoasDemo.Configuration.Links;
using HateoasDemo.Controllers;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace HateoasDemo.Configuration.LinkConfigurations
{
    public class ThoughtLinksDtoConfiguration : IConfigureOptions<TypeLinks>
    {
        public void Configure(TypeLinks links)
        {
            links.AddLink<ThoughtDto>(
                "collection",
                HttpMethod.Get,
                nameof(ThoughtsController.GetAll));
        }
    }
}
