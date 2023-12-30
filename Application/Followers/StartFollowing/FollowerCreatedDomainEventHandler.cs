using Application.Abstractions.Data;
using Application.Abstractions.Notifications;
using Application.Users;
using Domain.Followers.Events;
using Domain.Users;
using MediatR;

namespace Application.Followers.StartFollowing;

internal sealed class FollowerCreatedDomainEventHandler
    : INotificationHandler<FollowerCreatedDomainEvent>
{
    private readonly INotificationService _notificationService;
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly IApplicationDbContext _dbContext;

    public FollowerCreatedDomainEventHandler(
        INotificationService notificationService,
        IApplicationDbContext dbContext,
        IDbConnectionFactory connectionFactory)
    {
        _notificationService = notificationService;
        _dbContext = dbContext;
        _connectionFactory = connectionFactory;
    }

    public async Task Handle(FollowerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        UserDto user = await _dbContext.GetUserDtoAsync(notification.UserId, cancellationToken);

        await _notificationService.SendAsync(
            notification.FollowerId,
            $"{user.Name} started following you",
            cancellationToken);
    }
}
