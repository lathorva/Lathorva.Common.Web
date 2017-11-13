using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lathorva.Common.Web.Attributes
{
    public class CheckModelForNullAttribute : ActionFilterAttribute
    {
        private readonly Func<IDictionary<string, object>, bool> _validate;

        public CheckModelForNullAttribute() : this(arguments =>
            arguments.Any(e => e.Value == null))
        { }

        public CheckModelForNullAttribute(Func<IDictionary<string, object>, bool> checkCondition)
        {
            _validate = checkCondition;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (_validate(context.ActionArguments))
            {
                context.Result = new BadRequestObjectResult(new WrappedHttpError("The argument is either invalid or missing"));
            }
        }
    }
}
