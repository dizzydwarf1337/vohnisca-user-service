    using Domain.Interfaces.Messages;
    using Domain.Models.Chats;
    using LanguageExt;
    using LanguageExt.Common;
    using Microsoft.EntityFrameworkCore;
    using Persistence.Database;

    namespace Persistence.Repositories.Messages;

    public class MessageReadStatusRepository : IMessageReadStatusRepository
    {
        private readonly VohniscaDbContext _context;

        public MessageReadStatusRepository(VohniscaDbContext context)
        {
            _context = context;
        }

        public IQueryable<MessageReadStatus> GetAllEntities()
        {
            return _context.MessageReadStatuses.AsQueryable();
        }

        public async Task<Option<MessageReadStatus>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.MessageReadStatuses.FirstOrDefaultAsync(x => x.MessageId == id, cancellationToken);
        }

        public async Task<Either<Error, MessageReadStatus>> GetByCompositeKeyAsync(Guid messageId, Guid userId, CancellationToken cancellationToken)
        {
            var status = await _context.MessageReadStatuses
                .Include(mrs => mrs.Message)
                .Include(mrs => mrs.User)
                .FirstOrDefaultAsync(mrs => mrs.MessageId == messageId && mrs.UserId == userId, cancellationToken);

            return status != null
                ? status
                : Error.New("Read status not found");
        }

        public async Task<Either<Error, MessageReadStatus>> SaveAsync(MessageReadStatus entity, CancellationToken cancellationToken)
        {
            await _context.MessageReadStatuses.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<Either<Error, MessageReadStatus>> UpdateAsync(MessageReadStatus entity, CancellationToken cancellationToken)
        {
            _context.MessageReadStatuses.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public async Task<Either<Error, Unit>> DeleteAsync(MessageReadStatus entity, CancellationToken cancellationToken)
        {
            _context.MessageReadStatuses.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Default;
        }
    }