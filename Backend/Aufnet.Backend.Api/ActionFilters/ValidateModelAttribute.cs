using Aufnet.Backend.Api.Validation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aufnet.Backend.Api.ActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
    }
}
