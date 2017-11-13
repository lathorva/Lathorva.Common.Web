using System;
using System.Net;
using Lathorva.Common.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Lathorva.Common.Web
{
    public static class WebUtils
    {
        public static IActionResult CreateActionResult<TKey, TModel>(ICrudResult<TKey, TModel> crudResult)
            where TKey : IConvertible
            where TModel : class, IEntity<TKey>
        {
            switch (crudResult.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(crudResult.Model);
                case HttpStatusCode.Created:
                    return new CreatedAtRouteResult(nameof(TModel), new {id = crudResult.Model.Id}, crudResult.Model);
                case HttpStatusCode.NotFound:
                    return new NotFoundResult();
                case HttpStatusCode.Conflict:
                    //return new Conflic
                    return new ConflictObjectResult(new WrappedHttpError("Conflicted error"));
                default:
                    throw new ArgumentException($"Status: {crudResult.StatusCode} is not supported.");

            }
        }
    }
}
