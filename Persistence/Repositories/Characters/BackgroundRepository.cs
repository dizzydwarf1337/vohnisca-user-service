using Domain.Interfaces.Repositories.Characters;
using Domain.Models.Characters;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories.Characters;

public class BackgroundRepository : IBackgroundRepository
{
    private readonly VohniscaDbContext _context;
    
    public BackgroundRepository(VohniscaDbContext context)
        => _context = context;
    
    public Either<Error, IQueryable<Background>> GetAllEntities()
    {
        return Prelude.Right(_context.Backgrounds.AsQueryable());
    }

    public async Task<Either<Error, Background>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var background = await _context.Backgrounds.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (background == null)
            return Error.New("Background not found");
        return background;
    }

    public async Task<Either<Error, Background>> SaveAsync(Background entity, CancellationToken cancellationToken)
    {
        await _context.Backgrounds.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Background>> UpdateAsync(Background entity, CancellationToken cancellationToken)
    {
        _context.Backgrounds.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Unit>> DeleteAsync(Background entity, CancellationToken cancellationToken)
    {
        _context.Backgrounds.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Default;
    }
}