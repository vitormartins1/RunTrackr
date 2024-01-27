using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Users.Create;

public sealed record CreateUserCommand(string Email, string Name, bool HasPublicProfile)
    : ICommand<Guid>;
