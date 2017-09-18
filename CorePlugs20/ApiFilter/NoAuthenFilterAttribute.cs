using Microsoft.AspNetCore.Mvc.Filters;

namespace CorePlugs20.ApiFilter
{
    public class NoAuthenFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
    }
}