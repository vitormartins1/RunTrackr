using Domain.Abstractions;

namespace Domain.Followers;

public sealed class Follower : Entity
{
    internal Follower(Guid userId, Guid followerId, DateTime createOnUtc)
    {
        UserId = userId;
        FollowerId = followerId;
        CreateOnUtc = createOnUtc;
    }

    private Follower()
    {

    }

    public Guid UserId { get; private set; }
    public Guid FollowerId { get; private set; }
    public DateTime CreateOnUtc { get; private set; }
}