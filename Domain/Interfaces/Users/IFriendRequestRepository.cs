using Domain.Models.Users;
using LanguageExt;
namespace Domain.Interfaces.Users;

public interface IFriendRequestRepository : IBaseRepository<FriendRequest>
{
    Task<Option<FriendRequest>> GetByCompositeKeyAsync(Guid firstUserId,  Guid secondUserId, CancellationToken cancellationToken);
}