using Domain.Interfaces.Repositories.Characters;
using Domain.Models.Characters;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories.Characters;

public class RaceRepository : IRaceRepository
{
    private readonly VohniscaDbContext _context;
    public RaceRepository(VohniscaDbContext context)
        => _context = context;
    
    public Either<Error, IQueryable<Race>> GetAllEntities()
    {
        return Prelude.Right(_context.Races.AsQueryable());
    }

    public async Task<Either<Error, Race>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var race = await _context.Races.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (race == null)
            return Error.New("Race not found");
        
        return race;
    }

    public async Task<Either<Error, Race>> SaveAsync(Race entity, CancellationToken cancellationToken)
    {
        await _context.Races.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Race>> UpdateAsync(Race entity, CancellationToken cancellationToken)
    {
        _context.Races.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Unit>> DeleteAsync(Race entity, CancellationToken cancellationToken)
    {
        _context.Races.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Default;
    }
}