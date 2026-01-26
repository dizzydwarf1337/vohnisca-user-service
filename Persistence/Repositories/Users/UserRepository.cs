using Domain.Interfaces.Users;
using Domain.Models.Users;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly VohniscaDbContext _context;
    public UserRepository(VohniscaDbContext context)
        => _context = context;
    
    public IQueryable<User> GetAllEntities()
    {
        return _context.Users.AsQueryable();
    }

    public async Task<Option<User>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Either<Error, User>> SaveAsync(User entity, CancellationToken cancellationToken)
    {
        await _context.Users.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, User>> UpdateAsync(User entity, CancellationToken cancellationToken)
    {
        _context.Users.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Unit>> DeleteAsync(User entity, CancellationToken cancellationToken)
    {
        _context.Users.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Default;
    }
}