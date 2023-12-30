namespace Application.Abstractions.Messaging;

public interface ICachedQuery<TResponse> : IQuery<TResponse>, ICachedQuery
{

}

public interface ICachedQuery
{
    public string Key { get; }
    public TimeSpan? Expiration { get; }
}
