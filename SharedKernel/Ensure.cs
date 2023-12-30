using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace SharedKernel;

public static class Ensure
{
    public static void NotNullOrWhiteSpace(string? value, string? paramName = null)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("The value can't be null", paramName);
        }
    }

    public static void NotNullOrEmpty(
    [NotNull] string? value,
    [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(paramName);
        }
    }
}
