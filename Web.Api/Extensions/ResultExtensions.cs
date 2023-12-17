using SharedKernel;

namespace Web.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Can't convert success result to problem");
        }

        return Results.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Bad Request",
                type: "",
                extensions: new Dictionary<string, object?>
                {
                    { "errors", new[] { result.Error} }
                });
    }
}
