using SharedKernel;

namespace Domain.Users;

public static class UserErrors
{
    public static Error EmailNotUnique => Error.Conflict("User.EmailNotUnique", "The provided email is not unique");

    public static Error NotFound(Guid userId) => Error.NotFound(
        "User.NotFound", $"The user with the Id = '{userId}' was not found");
    
    public static Error NotFoundByEmail(string email) => Error.NotFound(
        "User.NotFoundByEmail", $"The user with the Email = '{email}' was not found");
}