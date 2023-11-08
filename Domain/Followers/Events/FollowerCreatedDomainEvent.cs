using Domain.Abstractions;

namespace Domain.Followers.Events;

public sealed record FollowerCreatedDomainEvent(Guid UserId, Guid FollowerId) : IDomainEvent;