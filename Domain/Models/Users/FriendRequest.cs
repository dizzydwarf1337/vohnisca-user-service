using Domain.Models.Users.Enums;
using LanguageExt;
using LanguageExt.Common;

namespace Domain.Models.Users;

public class FriendRequest
{
    public Guid Id { get; set; }
    
    public Guid SentBy { get; set; }
    
    public Guid SentTo { get; set; }
    
    public DateTime SentAt { get; set; }
    
    public DateTime? StatusChangedAt { get; set; }
    
    public FriendRequestStatus Status { get; set; }
    
    public virtual User SentByUser { get; set; }
    
    public virtual User SentToUser { get; set; }

    public static Either<Error, FriendRequest> Create(Guid sentBy, Guid sentTo)
    {
        if (sentBy == sentTo)
            return Error.New("You can't send friend request to yourself");

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
        if (Status == FriendRequestStatus.Rejected)
            return Error.New("You can't accept rejected request");
        if (Status == FriendRequestStatus.Accepted)
            return Error.New("Friend request already accepted");
        
        StatusChangedAt = DateTime.UtcNow;
        Status = FriendRequestStatus.Accepted;
        return this;
    }

    public Either<Error, FriendRequest> Reject()
    {
        if (Status == FriendRequestStatus.Rejected)
            return Error.New("Friend request already rejected");
        if (Status == FriendRequestStatus.Accepted)
            return Error.New("You can't reject accepted request");
        
        StatusChangedAt = DateTime.UtcNow;
        Status = FriendRequestStatus.Rejected;
        return this;
    }
}