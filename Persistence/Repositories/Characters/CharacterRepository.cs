using Domain.Interfaces.Repositories.Characters;
using Domain.Models.Characters;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories.Characters;

public class CharacterRepository : ICharacterRepository
{
    private readonly VohniscaDbContext _context;
    public CharacterRepository(VohniscaDbContext context)
        => _context = context;


    public Either<Error, IQueryable<Character>> GetAllEntities()
    {
        return Prelude.Right(_context.Characters.AsQueryable());
    }

    public async Task<Either<Error, Character>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (character == null)
            return Error.New("Character not found");
        return character;
    }

    public async Task<Either<Error, Character>> SaveAsync(Character entity, CancellationToken cancellationToken)
    {
        await _context.Characters.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Character>> UpdateAsync(Character entity, CancellationToken cancellationToken)
    {
        _context.Characters.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Unit>> DeleteAsync(Character entity, CancellationToken cancellationToken)
    {
        _context.Characters.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Default;
    }
}