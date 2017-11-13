using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lathorva.Common.Web.Attributes
{
    /// <summary>
    /// For model validation, so we don't have to add the code for each action.
    /// </summary>
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var modelState = context.ModelState;

            if (!modelState.IsValid)
            {
                var error = new WrappedHttpError(modelState);

                context.Result = new BadRequestObjectResult(error);
            }
        }
    }
}
