using System.Collections.Generic;
using System.Linq;
using Lathorva.Common.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Lathorva.Common.Web
{
    /// <summary>
    /// The purpose is to wrap the errors in a custom object, instead of using the default modelstate which exposes more than we want to
    /// 
    /// http://stackoverflow.com/a/17114496/3674201
    /// </summary>
    public class WrappedHttpError
    {
        public WrappedHttpError(string message)
        {
            Message = message;
        }
        public WrappedHttpError(ModelStateDictionary modelState)
        {
            Message = "Your request is invalid.";
            Errors = new Dictionary<string, IEnumerable<string>>();

            foreach (var item in modelState)
            {
                var itemErrors = new List<string>();
                foreach (var childItem in item.Value.Errors)
                {
                    itemErrors.Add(string.IsNullOrEmpty(childItem.ErrorMessage) ? "Format error" : childItem.ErrorMessage);
                }

                // So that every property starts with a lowercase.
                var errArr = item.Key.Split('.').Skip(item.Key.Contains(".") ? 1 : 0).Select(e => e.LowercaseFirst()).ToArray();

                if (itemErrors.Any())
                {
                    var key = string.Join(".", errArr);

                    if (!Errors.ContainsKey(key)) Errors.Add(key, itemErrors);
                }
            }
        }

        public WrappedHttpError(string message, IDictionary<string, IEnumerable<string>> errors)
        {
            Message = message;
            Errors = errors;
        }

        public string Message { get; set; }

        public IDictionary<string, IEnumerable<string>> Errors { get; set; }
    }
}
