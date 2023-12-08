using Domain.Followers;
using Domain.Users;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace Domain.UnitTests.Followers;

public class FollowerServiceTests
{
    private readonly FollowerService _followerService;
    private readonly IFollowerRepository _followerRepositoryMock;
    private static readonly Email Email = Email.Create("test@test.com").Value;
    private static readonly Name Name = new Name("Full name");
    private static readonly DateTime UtcNow = DateTime.UtcNow;

    public FollowerServiceTests()
    {
        _followerRepositoryMock = Substitute.For<IFollowerRepository>();

        var dateTimeProvider = Substitute.For<IDateTimeProvider>();
        dateTimeProvider.UtcNow.Returns(UtcNow);

        _followerService = new FollowerService(_followerRepositoryMock, dateTimeProvider);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenFollowingSameUser()
    {
        var user = User.Create(Name, Email, false);

        var result = await _followerService.StartFollowingAsync(
            user,
            user,
            default);

        result.Error.Should().Be(FollowerErrors.SameUser);
    }

    [Fact]
    public async Task StarFollowingAsync_Should_ReturnError_WhenFollowingNonPublicProfile()
    {
        var user = User.Create(Name, Email, false);
        var followed = User.Create(Name, Email, false);

        var result = await _followerService.StartFollowingAsync(
            user,
            followed,
            default);

        result.Error.Should().Be(FollowerErrors.NonPublicProfile);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenAlreadyFollowing()
    {

        var user = User.Create(Name, Email, false);
        var followed = User.Create(Name, Email, true);

        _followerRepositoryMock
            .IsAlreadyFollowingAsync(user.Id, followed.Id, default)
            .Returns(true);

        var result = await _followerService.StartFollowingAsync(
            user,
            followed,
            default);

        result.Error.Should().Be(FollowerErrors.AlreadyFollowing);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenFollowerCreated()
    {
        var user = User.Create(Name, Email, false);
        var followed = User.Create(Name, Email, true);

        _followerRepositoryMock
            .IsAlreadyFollowingAsync(user.Id, followed.Id, default)
            .Returns(false);

        var result = await _followerService.StartFollowingAsync(
            user,
            followed,
            default);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task StartFollowingAsync_Should_CallInsertOnRepository_WhenFollowerCreated()
    {
        var user = User.Create(Name, Email, false);
        var followed = User.Create(Name, Email, true);

        _followerRepositoryMock
            .IsAlreadyFollowingAsync(user.Id, followed.Id, default)
            .Returns(false);

        await _followerService.StartFollowingAsync(
            user,
            followed,
            default);

        _followerRepositoryMock
            .Received(1)
            .Insert(Arg.Is<Follower>(f => f.UserId == user.Id && 
                                          f.FollowerId == followed.Id &&
                                          f.CreateOnUtc == UtcNow));
    }
}