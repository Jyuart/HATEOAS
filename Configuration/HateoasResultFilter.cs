using HateoasDemo.Configuration.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HateoasDemo.Configuration
{
    public class HateoasResultFilter : IResultFilter
    {
        private readonly IHateoasService _hateoasService;

        public HateoasResultFilter(IHateoasService hateoasService)
        {
            _hateoasService = hateoasService;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is not ObjectResult objectResult) return;

            context.HttpContext.Response.ContentType = "application/hal+json";
            objectResult.Value = _hateoasService.GenerateHateoasResponse(objectResult.Value, context);
            context.Result = objectResult;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}