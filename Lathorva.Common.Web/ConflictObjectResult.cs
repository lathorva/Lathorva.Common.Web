using Microsoft.AspNetCore.Mvc;

namespace Lathorva.Common.Web
{
    public class ConflictObjectResult : ObjectResult
    {
        public ConflictObjectResult(object value) : base(value)
        {
            StatusCode = 409;
        }
    }
}
