using Application.Abstractions.Data;
using Application.Followers.StartFollowing;
using Domain.Followers;
using Domain.Users;
using FluentAssertions;
using NSubstitute;
using SharedKernel;
using System.Runtime.CompilerServices;

namespace Application.UnitTests.Followers;

public class StartFollowingCommandTests
{
    private static readonly StartFollowingCommand Command = new(Guid.NewGuid(), Guid.NewGuid());
    private static readonly User User = User.Create(
        new Name("FullName"),
        Email.Create("test@test.com").Value,
        true);

    private readonly StartFollowingCommandHandler _handler;
    private readonly IUserRepository _userRepositoryMock;
    private readonly IFollowerService _followerServiceMock;
    private readonly IUnitOfWork _unitOfWorkMock;

    public StartFollowingCommandTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _followerServiceMock = Substitute.For<IFollowerService>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new StartFollowingCommandHandler(
            _userRepositoryMock,
            _followerServiceMock,
            _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenTheUserIsNull()
    {
        _userRepositoryMock.GetByIdAsync(Command.UserId).Returns((User?)null);

        Result result = await _handler.Handle(Command, default);

        result.Error.Should().Be(UserErrors.NotFound(Command.UserId));
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenTheFollowedUserIsNull()
    {
        _userRepositoryMock.GetByIdAsync(Command.UserId).Returns(User);
        _userRepositoryMock.GetByIdAsync(Command.FollowedId).Returns((User?)null);

        Result result = await _handler.Handle(Command, default);

        result.Error.Should().Be(UserErrors.NotFound(Command.FollowedId));
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenStartFollowingFails()
    {
        _userRepositoryMock.GetByIdAsync(Command.UserId).Returns(User);
        _userRepositoryMock.GetByIdAsync(Command.FollowedId).Returns(User);

        _followerServiceMock.StartFollowingAsync(User, User, default)
            .Returns(FollowerErrors.SameUser);

        Result result = await _handler.Handle(Command, default);

        result.Error.Should().Be(FollowerErrors.SameUser);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenStartFollowingDoesNotFail()
    {
        _userRepositoryMock.GetByIdAsync(Command.UserId).Returns(User);
        _userRepositoryMock.GetByIdAsync(Command.FollowedId).Returns(User);

        _followerServiceMock.StartFollowingAsync(User, User, default)
            .Returns(Result.Success());

        Result result = await _handler.Handle(Command, default);

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenStartFollowingDoesNotFail()
    {
        _userRepositoryMock.GetByIdAsync(Command.UserId).Returns(User);
        _userRepositoryMock.GetByIdAsync(Command.FollowedId).Returns(User);

        _followerServiceMock.StartFollowingAsync(User, User, default)
            .Returns(Result.Success());

        await _handler.Handle(Command, default);

        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
