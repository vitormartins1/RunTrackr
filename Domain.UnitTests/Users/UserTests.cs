using Domain.Users;
using Domain.Users.Events;
using FluentAssertions;

namespace Domain.UnitTests.Users;

public class UserTests
{
    [Fact]
    public void Create_Should_ReturnUser_WhenNameIsValid()
    {
        var name = new Name("Full name");
        var email = Email.Create("test@test.com");

        var user = User.Create(name, email, true);

        user.Should().NotBeNull();
    }

    [Fact]
    public void Create_Should_RaiseDomainEvent_WhenNameIsValid()
    {
        var name = new Name("Full name");
        var email = Email.Create("test@test.com");

        var user = User.Create(name, email, true);

        user.DomainEvents
            .Should().ContainSingle()
            .Which.Should().BeOfType<UserCreatedDomainEvent>();
    }
}