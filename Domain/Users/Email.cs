
namespace Domain.Users;

public sealed record Email
{
    private Email(string? value)
    {
        Ensure.NotNullOrEmpty(value);
        Value = value;
    }

    public string Value { get; }

    public static Email Create(string v)
    {
        return new Email(v);
    }
}