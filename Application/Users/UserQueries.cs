using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;

namespace Application.Users;

public static class UserQueries
{
    public static async Task<UserDto> GetUserDtoAsync(
        this IApplicationDbContext dbContext,
        Guid id,
        CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .Where(u => u.Id == id)
            .Select(u => new UserDto(u.Id, u.Name.Value))
            .SingleAsync(cancellationToken);
    }
}