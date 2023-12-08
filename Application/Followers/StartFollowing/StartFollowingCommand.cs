using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Followers;
using Domain.Users;
using SharedKernel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;
using ICommand = Application.Abstractions.Messaging.ICommand;

namespace Application.Followers.StartFollowing;

public sealed record StartFollowingCommand(Guid UserId, Guid FollowerId) : ICommand;

internal sealed class StartFollowingCommandHandler : ICommandHandler<StartFollowingCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly FollowerService _followerService;
    private readonly IUnitOfWork _unitOfWork;

    public StartFollowingCommandHandler(
        IUserRepository userRepository,
        FollowerService followerService,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _followerService = followerService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(StartFollowingCommand command, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
        if (user is null) 
        { 
            return UserErrors.NotFound(command.UserId);
        }

        User? followed = await _userRepository.GetByIdAsync(command.FollowerId, cancellationToken);
        if (followed is null)
        {
            return UserErrors.NotFound(command.FollowerId);
        }

        Result result = await _followerService.StartFollowingAsync(
            user, 
            followed, 
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid UserId, CancellationToken cancellation = default);
}

public static class UserErrors
{
    public static Error NotFound(Guid userId) => new(
        "User.NotFound", $"The user with the Id = '{userId}' was not found");
}