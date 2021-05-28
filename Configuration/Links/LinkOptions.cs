using System;
using System.Net.Http;

namespace HateoasDemo.Configuration.Links
{
    public class LinkOptions<T> : ILinkOptions
    {
        private readonly Func<T, object> _getRouteValues;
        private readonly Func<T, string, string> _templateLinkGenerator;
        private readonly Func<T, bool> _canCreateLink;
        private readonly string _template;

        public LinkOptions(
            string relation,
            HttpMethod httpMethod,
            string routeName,
            Func<T, object> getRouteValues,
            Func<T, bool> canCreateLink)
        {
            Relation = relation;
            HttpMethod = httpMethod;
            RouteName = routeName;
            _getRouteValues = getRouteValues;
            _canCreateLink = canCreateLink;
        }

        public LinkOptions(
            string relation,
            HttpMethod httpMethod,
            string template,
            Func<T, string, string> templateLinkGenerator,
            Func<T, bool> canCreateLink)
        {
            Relation = relation;
            HttpMethod = httpMethod;
            _template = template;
            _templateLinkGenerator = templateLinkGenerator;
            _canCreateLink = canCreateLink;
        }

        public string Relation { get; }
        public string RouteName { get; }
        public HttpMethod HttpMethod { get; }
        public bool IsTemplate => !string.IsNullOrEmpty(_template);
        public object GetRouteValues(object obj) => _getRouteValues((T)obj);

        public string GetLinkTemplate(object obj) => _templateLinkGenerator((T)obj, _template);
        public bool CanCreateLink(object obj) => _canCreateLink((T)obj);
    }
}
