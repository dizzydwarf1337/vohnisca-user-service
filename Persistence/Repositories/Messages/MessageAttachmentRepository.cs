using Domain.Interfaces.Messages;
using Domain.Models.Chats;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories.Messages;

public class MessageAttachmentRepository : IMessageAttachmentRepository
{
    private readonly VohniscaDbContext _context;

    public MessageAttachmentRepository(VohniscaDbContext context)
    {
        _context = context;
    }

    public IQueryable<MessageAttachment> GetAllEntities()
    {
        return _context.MessageAttachments.AsQueryable();
    }

    public async Task<Option<MessageAttachment>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.MessageAttachments
            .Include(a => a.Message)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<Either<Error, MessageAttachment>> SaveAsync(MessageAttachment entity, CancellationToken cancellationToken)
    {
        await _context.MessageAttachments.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, MessageAttachment>> UpdateAsync(MessageAttachment entity, CancellationToken cancellationToken)
    {
        _context.MessageAttachments.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Unit>> DeleteAsync(MessageAttachment entity, CancellationToken cancellationToken)
    {
        _context.MessageAttachments.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Default;
    }
}