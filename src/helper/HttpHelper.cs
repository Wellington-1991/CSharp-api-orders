using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace api_orders.Helpers;

public static class HttpResponseHelper
{
    public static ActionResult Ok(object? data)
    {
        return new OkObjectResult(data);
    }

    public static ActionResult Created(object? data)
    {
        return new CreatedResult(string.Empty, data);
    }

    public static ActionResult BadRequest(object? data)
    {
        return new BadRequestObjectResult(data);
    }

    public static ActionResult NotFound(object? data)
    {
        return new NotFoundObjectResult(data);
    }

    public static ActionResult ServerError(object? data)
    {
        return new ObjectResult(data)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}
