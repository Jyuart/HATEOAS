using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using HateoasDemo.Configuration.Links;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace HateoasDemo.Configuration.Service
{
    public class HateoasService : IHateoasService
    {
        private readonly IOptions<TypeLinks> _typeLinks;
        private readonly LinkGenerator _linkGenerator;

        public HateoasService(IOptions<TypeLinks> typeLinks, LinkGenerator linkGenerator)
        {
            _typeLinks = typeLinks;
            _linkGenerator = linkGenerator;
        }

        public object GenerateHateoasResponse(object value, ActionContext context)
        {
            if (value == null)
            {
                return null;
            }

            IDictionary<string, object> result = AddPropertiesAndEmbedded(value, context);
            IDictionary<string, object> links = AddResourceLinks(value, context);

            if (links is not null)
            {
                result["_links"] = AddResourceLinks(value, context);
            }

            return result;
        }

        private IDictionary<string, object> AddPropertiesAndEmbedded(object obj, ActionContext context)
        {
            IDictionary<string, object> properties = new ExpandoObject();
            IDictionary<string, object> embedded = new ExpandoObject();

            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                var propertyValue = property.GetValue(obj);
                var propertyName = property.Name;
                var propertyType = property.PropertyType;

                if (propertyValue is null)
                {
                    continue;
                }

                if (!_typeLinks.Value.HasLinks(propertyType) && propertyValue is not IEnumerable<object>)
                {
                    properties.Add(propertyName, propertyValue);
                    continue;
                }

                var content = propertyValue is IEnumerable<object> list
                    ? list.ToList().Select(x => GenerateHateoasResponse(x, context)).ToList()
                    : GenerateHateoasResponse(propertyValue, context);

                embedded.Add(propertyName, content);
            }

            if (embedded.Count > 0)
            {
                properties["_embedded"] = embedded;
            }

            return properties;
        }

        private IDictionary<string, object> AddResourceLinks(object obj, ActionContext context)
        {
            var linkOptions = _typeLinks.Value.GetLinks(obj.GetType());

            return linkOptions?
                .Where(x => x.CanCreateLink(obj))
                .ToDictionary(link => link.Relation, link => new
            {
                href = link.IsTemplate 
                    ? link.GetLinkTemplate(obj)
                    : new Uri(_linkGenerator.GetUriByRouteValues(context.HttpContext, link.RouteName, link.GetRouteValues(obj)) ?? string.Empty).PathAndQuery,
                method = link.HttpMethod.Method,
                templated = link.IsTemplate
            } as object);
        }
    }
}
