using Domain.Abstractions;
using Domain.Users.Events;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Users;

public sealed class User : Entity
{
    private User(Guid id, Name name, Email email, bool hasPublicProfile) 
        : base(id)
    {
        Name = name;
        Email = email;
        HasPublicProfile = hasPublicProfile;
    }

    private User() : base(Guid.Empty)
    {
        
    }

    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public bool HasPublicProfile { get; internal set; }

    public static User Create(Name name, Email email, bool hasPublicProfile)
    {
        var user = new User(Guid.NewGuid(), name, email, hasPublicProfile);

        user.Raise(new UserCreatedDomainEvent(user.Id));

        return user;
    }
}