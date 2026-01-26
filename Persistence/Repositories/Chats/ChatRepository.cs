using Domain.Interfaces.Chats;
using Domain.Models.Chats;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories.Chats;

public class ChatRepository : IChatRepository
{
    private readonly VohniscaDbContext _context;

    public ChatRepository(VohniscaDbContext context)
    {
        _context = context;
    }

    public IQueryable<Chat> GetAllEntities()
    {
        return _context.Chats.AsQueryable();
    }

    public async Task<Option<Chat>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Chats
            .Include(c => c.CreatedBy)
            .Include(c => c.Participants)
            .Include(c => c.PinnedMessage)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Either<Error, Chat>> SaveAsync(Chat entity, CancellationToken cancellationToken)
    {
        await _context.Chats.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Chat>> UpdateAsync(Chat entity, CancellationToken cancellationToken)
    {
        _context.Chats.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Unit>> DeleteAsync(Chat entity, CancellationToken cancellationToken)
    {
        _context.Chats.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Default;
    }
}