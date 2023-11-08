using Domain.Users;
using FluentAssertions;

namespace Domain.UnitTests.Users;

public class UserTests
{
    [Fact]
    public void Create_Should_ReturnUser_WhenNameIsValid()
    {
        var name = new Name("Full name");

        var user = User.Create(name);

        user.Should().NotBeNull();
    }

    [Fact]
    public void Create_Should_RaiseDomainEvent_WhenNameIsValid()
    {
        var name = new Name("Full name");

        var user = User.Create(name);

        user.DomainEvents
            .Should().ContainSingle()
            .Which.Should().BeOfType<UserCreatedDomainEvent>();
    }
}