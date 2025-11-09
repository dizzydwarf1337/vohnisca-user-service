using Domain.Interfaces.Repositories.Characters;
using Domain.Models.Characters;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories.Characters;

public class SpellRepository : ISpellRepository
{
    private readonly VohniscaDbContext _context;
    public SpellRepository(VohniscaDbContext context)
        => _context = context;
    
    public Either<Error, IQueryable<Spell>> GetAllEntities()
    {
        return Prelude.Right(_context.Spells.AsQueryable());
    }

    public async Task<Either<Error, Spell>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var spell = await _context.Spells.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (spell == null)
            return Error.New("Spell not found");
        
        return spell;
    }

    public async Task<Either<Error, Spell>> SaveAsync(Spell entity, CancellationToken cancellationToken)
    {
        await _context.Spells.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Spell>> UpdateAsync(Spell entity, CancellationToken cancellationToken)
    {
        _context.Spells.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Unit>> DeleteAsync(Spell entity, CancellationToken cancellationToken)
    {
        _context.Spells.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Default;
    }
}