using MediatR;
using SharedKernel;

namespace Domain.Followers.Events;
public sealed record FollowerCreatedDomainEvent(Guid UserId, Guid FollowerId) : IDomainEvent, INotification;
