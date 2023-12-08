using Domain.Users;

namespace Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid UserId, CancellationToken cancellation = default);
    Task<bool> IsEmailUniqueAsync(Email email);
    void Insert(User user);
}
