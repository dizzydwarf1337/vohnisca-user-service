using Domain.Interfaces.Repositories.Characters;
using Domain.Models.Characters;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories.Characters;

public class ClassRepository : IClassRepository
{
    private readonly VohniscaDbContext _context;
    public ClassRepository(VohniscaDbContext context)
        => _context = context;
    
    public Either<Error, IQueryable<Class>> GetAllEntities()
    {
        return Prelude.Right(_context.Classes.AsQueryable());
    }

    public async Task<Either<Error, Class>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var @class = await _context.Classes.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (@class == null)
            return Error.New("Class not found");
        
        return @class;
    }

    public async Task<Either<Error, Class>> SaveAsync(Class entity, CancellationToken cancellationToken)
    {
        await _context.Classes.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Class>> UpdateAsync(Class entity, CancellationToken cancellationToken = default)
    {
        _context.Classes.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Unit>> DeleteAsync(Class entity, CancellationToken cancellationToken = default)
    {
        _context.Classes.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Default;
    }
}