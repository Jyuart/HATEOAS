using System;
using System.Collections.Generic;
using System.Net.Http;

namespace HateoasDemo.Configuration.Links
{
    public class TypeLinks
    {
        private readonly Dictionary<Type, Dictionary<string, ILinkOptions>> _links;
        internal IEnumerable<ILinkOptions> GetLinks(Type type) => _links.ContainsKey(type) ? _links[type].Values : null;
        internal bool HasLinks(Type type) => _links.ContainsKey(type);

        public TypeLinks()
        {
            _links = new Dictionary<Type, Dictionary<string, ILinkOptions>>();
        }

        internal void AddLinkTemplate<T>(string relation, HttpMethod httpMethod, string template, Func<T, string, string> templateGenerator, Func<T, bool> canCreate)
        {
            var type = typeof(T);
            if (!_links.ContainsKey(type))
            {
                _links[type] = new Dictionary<string, ILinkOptions>();
            }

            _links[type][relation] = new LinkOptions<T>(relation, httpMethod, template, templateGenerator, canCreate);
        }

        internal void AddLink<T>(string relation, HttpMethod httpMethod, string routeName, Func<T, object> routeValues, Func<T, bool> canCreate)
        {
            var type = typeof(T);
            if (!_links.ContainsKey(type))
            {
                _links[type] = new Dictionary<string, ILinkOptions>();
            }

            _links[type][relation] = new LinkOptions<T>(relation, httpMethod, routeName, routeValues, canCreate);
        }

        internal void AddLink<T>(string relation, HttpMethod httpMethod, string routeName)
            => AddLink<T>(relation, httpMethod, routeName, _ => null, _ => true);

        internal void AddLink<T>(string relation, HttpMethod httpMethod, string routeName, Func<T, object> routeValues)
            => AddLink(relation, httpMethod, routeName, routeValues, _ => true);

        internal void AddLinkTemplate<T>(string relation, HttpMethod httpMethod, string template, Func<T, string, string> templateGenerator)
            => AddLinkTemplate(relation, httpMethod, template, templateGenerator, _ => true);
    }
}
