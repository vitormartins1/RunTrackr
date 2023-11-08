using Domain.Users;
using FluentAssertions;

namespace Domain.UnitTests.Users;

public class NameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_Should_ThrowArgumentException_WhenValueIsInvalid(string? value)
    {
        Name Action() => new Name(value);

        FluentActions.Invoking(Action).Should().Throw<ArgumentNullException>()
            .Which.ParamName.Should().Be("value");
    }
}