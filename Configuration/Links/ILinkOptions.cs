using System.Net.Http;

namespace HateoasDemo.Configuration.Links
{
    public interface ILinkOptions
    {
        string Relation { get; }

        string RouteName { get; }

        HttpMethod HttpMethod { get; }

        bool IsTemplate { get; }

        object GetRouteValues(object obj);

        string GetLinkTemplate(object obj);

        bool CanCreateLink(object obj);
    }
}
