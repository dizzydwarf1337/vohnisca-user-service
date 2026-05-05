using Domain.Interfaces.Users;
using Domain.Models.Users;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories.Users;

public class FriendRequestRepository : IFriendRequestRepository
{
    private readonly VohniscaDbContext _context;
    public FriendRequestRepository(VohniscaDbContext context)
        => _context = context;
    
    public IQueryable<FriendRequest> GetAllEntities()
    {
        return _context.FriendRequests.AsQueryable();
    }

    public async Task<Option<FriendRequest>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.FriendRequests.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Option<FriendRequest>> GetByCompositeKeyAsync(Guid firstUserId, Guid secondUserId,
        CancellationToken cancellationToken)
    {
        return await _context.FriendRequests.FirstOrDefaultAsync(x => x.SentBy == firstUserId && x.SentTo == secondUserId, cancellationToken);
    }

    public async Task<Either<Error, FriendRequest>> SaveAsync(FriendRequest entity, CancellationToken cancellationToken)
    {
        await _context.FriendRequests.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, FriendRequest>> UpdateAsync(FriendRequest entity, CancellationToken cancellationToken)
    {
        _context.FriendRequests.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Unit>> DeleteAsync(FriendRequest entity, CancellationToken cancellationToken)
    {
        _context.FriendRequests.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Default;
    }
}