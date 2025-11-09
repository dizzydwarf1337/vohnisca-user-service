using Domain.Interfaces.Repositories.Characters;
using Domain.Models.Characters.Items;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories.Characters;

public class ItemRepository : IItemRepository
{
    private readonly VohniscaDbContext _context;
    public ItemRepository(VohniscaDbContext context)
        => _context = context;
    
    public Either<Error, IQueryable<Item>> GetAllEntities()
    {
        return Prelude.Right(_context.Items.AsQueryable());
    }

    public async Task<Either<Error, Item>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var item = await _context.Items.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (item == null)
            return Error.New("Item not found");
        
        return item;
    }

    public async Task<Either<Error, Item>> SaveAsync(Item entity, CancellationToken cancellationToken)
    {
        await _context.Items.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Item>> UpdateAsync(Item entity, CancellationToken cancellationToken)
    {
        _context.Items.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Unit>> DeleteAsync(Item entity, CancellationToken cancellationToken)
    {
        _context.Items.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Default;
    }
}