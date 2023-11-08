using Domain.Abstractions;
using Domain.Users.Events;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Users;

public sealed class User : Entity
{
    private User(Guid id, Name name) 
        : base(id)
    {
        Name = name;
    }

    private User() : base(Guid.Empty)
    {
        
    }

    public Name Name { get; private set; }
    public bool HasPublicProfile { get; internal set; }

    public static User Create(Name name)
    {
        var user = new User(Guid.NewGuid(), name);

        user.Raise(new UserCreatedDomainEvent(user.Id));

        return user;
    }
}