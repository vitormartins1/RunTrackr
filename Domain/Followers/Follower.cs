using Domain.Abstractions;
using Domain.Followers.Events;

namespace Domain.Followers;

public sealed class Follower : Entity
{
    private Follower(Guid userId, Guid followerId, DateTime createOnUtc)
        : base(Guid.NewGuid())
    {
        UserId = userId;
        FollowerId = followerId;
        CreateOnUtc = createOnUtc;
    }

    private Follower() : base(Guid.Empty)
    {

    }

    public Guid UserId { get; private set; }
    public Guid FollowerId { get; private set; }
    public DateTime CreateOnUtc { get; private set; }

    public static Follower Create(Guid userId, Guid followedId, DateTime createOnUtc)
    {
        var follower = new Follower(userId, followedId, createOnUtc);

        follower.Raise(new FollowerCreatedDomainEvent(follower.UserId, follower.FollowerId));

        return follower;
    }
}
