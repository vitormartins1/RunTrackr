using ICommand = Application.Abstractions.Messaging.ICommand;

namespace Application.Followers.StartFollowing;

public sealed record StartFollowingCommand(Guid UserId, Guid FollowerId) : ICommand;
