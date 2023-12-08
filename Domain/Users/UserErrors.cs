using SharedKernel;

namespace Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid userId) => new(
        "User.NotFound", $"The user with the Id = '{userId}' was not found");
    
    public static Error NotFoundByEmail(string email) => new(
        "User.NotFoundByEmail", $"The user with the Email = '{email}' was not found");
}