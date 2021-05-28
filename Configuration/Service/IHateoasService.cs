using Microsoft.AspNetCore.Mvc;

namespace HateoasDemo.Configuration.Service
{
    public interface IHateoasService
    {
        public object GenerateHateoasResponse(object value, ActionContext context);
    }
}
