using Domain.Followers;
using Domain.Users;
using FluentAssertions;

namespace Domain.UnitTests.Followers;

public class FollowerServiceTests
{
    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenFollowingSameUser()
    {
        var followerService = new FollowerService(null);
        var email = Email.Create("test@test.com");
        var name = new Name("Full name");

        var user = User.Create(name, email, false);

        var result = await followerService.StartFollowing(
            user,
            user,
            DateTime.Now,
            default);

        result.Error.Should().Be(FollowerErrors.SameUser);
    }
}