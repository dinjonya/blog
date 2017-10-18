using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CorePlugs20.ApiFilter
{
    public class UiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        public override void OnException(ExceptionContext context)
        {
            System.Console.WriteLine("OnException");
            if (!_hostingEnvironment.IsDevelopment())
            {
                // do nothing
                return;
            }
            if (!context.ExceptionHandled) {
                context.Result = new RedirectResult("/");
            }
            context.ExceptionHandled = true; // mark exception as handled
        }
    }
}