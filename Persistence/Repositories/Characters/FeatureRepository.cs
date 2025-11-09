using Domain.Interfaces.Repositories.Characters;
using Domain.Models.Characters;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories.Characters;

public class FeatureRepository : IFeatureRepository
{
    private readonly VohniscaDbContext _context;
    public FeatureRepository(VohniscaDbContext context)
        => _context = context;
    
    public Either<Error, IQueryable<Feature>> GetAllEntities()
    {
        return Prelude.Right(_context.Features.AsQueryable());
    }

    public async Task<Either<Error, Feature>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var feature = await _context.Features.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (feature == null)
            return Error.New("Feature not found");
        
        return feature;
    }

    public async Task<Either<Error, Feature>> SaveAsync(Feature entity, CancellationToken cancellationToken)
    {
        await _context.Features.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Feature>> UpdateAsync(Feature entity, CancellationToken cancellationToken)
    {
        _context.Features.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Unit>> DeleteAsync(Feature entity, CancellationToken cancellationToken)
    {
        _context.Features.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Default;
    }
}