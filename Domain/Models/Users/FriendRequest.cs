using Domain.Models.Users.Enums;
using LanguageExt;
using LanguageExt.Common;

namespace Domain.Models.Users;

public class FriendRequest
{
    public Guid Id { get; init; }
    
    public Guid SentBy { get; init; }
    
    public Guid SentTo { get; init; }
    
    public DateTime SentAt { get; init; }
    
    public DateTime? StatusChangedAt { get; private set; }
    
    public FriendRequestStatus Status { get; private set; }
    
    public virtual User SentByUser { get; set; }
    
    public virtual User SentToUser { get; set; }

    public static Either<Error, FriendRequest> Create(Guid sentBy, Guid sentTo)
    {
        if (sentBy == sentTo)
            return Error.New("You cannot send friend request to yourself");

        return new FriendRequest
        {
            SentBy = sentBy,
            SentTo = sentTo,
            SentAt = DateTime.UtcNow,
            Status = FriendRequestStatus.Pending,
        };
    }

    public Either<Error, FriendRequest> Accept()
    {
        if (Status == FriendRequestStatus.Accepted)
            return Error.New("Friend request already accepted.");
        if (Status != FriendRequestStatus.Pending)
            return Error.New("You cannot accept non pending request.");
        
        StatusChangedAt = DateTime.UtcNow;
        Status = FriendRequestStatus.Accepted;
        return this;
    }

    public Either<Error, FriendRequest> Reject()
    {
        if (Status == FriendRequestStatus.Rejected)
            return Error.New("Friend request already rejected.");
        if (Status != FriendRequestStatus.Pending)
            return Error.New("You cannot reject non pending request.");
        
        StatusChangedAt = DateTime.UtcNow;
        Status = FriendRequestStatus.Rejected;
        return this;
    }

    public Either<Error, FriendRequest> Cancel()
    {
        if (Status == FriendRequestStatus.Cancelled)
            return Error.New("Friend request already cancelled.");
        if (Status != FriendRequestStatus.Pending)
            return Error.New("You cannot cancel non pending request.");

        StatusChangedAt = DateTime.UtcNow;
        Status = FriendRequestStatus.Cancelled;
        return this;
    }

    public Either<Error, FriendRequest> Delete()
    {
        if (Status == FriendRequestStatus.Deleted)
            return Error.New("Friend request not found.");
        if (Status == FriendRequestStatus.Pending)
            return Error.New("You cannot delete pending request.");

        StatusChangedAt = DateTime.UtcNow;
        Status = FriendRequestStatus.Deleted;
        return this;
    }
}