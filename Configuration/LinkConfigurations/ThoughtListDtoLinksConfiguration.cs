using HateoasDemo.BL;
using HateoasDemo.Configuration.Links;
using HateoasDemo.Configuration.Templates;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace HateoasDemo.Configuration.LinkConfigurations
{
    public class ThoughtListDtoLinksConfiguration : IConfigureOptions<TypeLinks>
    {
        public void Configure(TypeLinks links)
        {
            links.AddLinkTemplate<ThoughtListDto>(
                "self",
                HttpMethod.Get,
                (ThoughtTemplates.Get),
                (_, template) => template,
                x => x.Total > 0
            );
        }
    }
}
