using Domain.Models.Chats;
using LanguageExt;
using LanguageExt.Common;

namespace Domain.Interfaces.Messages;

public interface IMessageReadStatusRepository : IBaseRepository<MessageReadStatus>
{
    Task<Either<Error, MessageReadStatus>> GetByCompositeKeyAsync(Guid messageId, Guid userId, CancellationToken cancellationToken);
}