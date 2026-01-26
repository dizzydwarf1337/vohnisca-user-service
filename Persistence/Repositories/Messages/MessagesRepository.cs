using Domain.Interfaces.Messages;
using Domain.Models.Chats;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories.Messages;

public class MessagesRepository : IMessageRepository
{
    private readonly VohniscaDbContext _context;

    public MessagesRepository(VohniscaDbContext context)
    {
        _context = context;
    }

    public IQueryable<Message> GetAllEntities()
    {
        return _context.Messages.AsQueryable();
    }

    public async Task<Option<Message>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Messages
            .Include(m => m.Sender)
            .Include(m => m.Chat)
            .Include(m => m.Attachments)
            .Include(m => m.ReadStatuses)
            .Include(m => m.ReplyToMessage)
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }

    public async Task<Either<Error, Message>> SaveAsync(Message entity, CancellationToken cancellationToken)
    {
        await _context.Messages.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Message>> UpdateAsync(Message entity, CancellationToken cancellationToken)
    {
        _context.Messages.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Unit>> DeleteAsync(Message entity, CancellationToken cancellationToken)
    {
        _context.Messages.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Default;
    }
}