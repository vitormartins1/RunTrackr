using Application.Users.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;

namespace Web.Api.Endpoints;

public static class UserEndpoint
{
    public static void MapUserEndPoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/users", async (CreateUserCommand Command, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(Command);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        });
    }
}
