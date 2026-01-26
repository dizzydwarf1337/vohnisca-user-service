using Domain.Interfaces.Notifications;
using Domain.Models.Notifications;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;

namespace Persistence.Repositories.Notifications;

public class NotificationRepository : INotificationRepository
{
    private readonly VohniscaDbContext _context;

    public NotificationRepository(VohniscaDbContext context)
    {
        _context = context;
    }

    public IQueryable<Notification> GetAllEntities()
    {
        return _context.Notifications.AsQueryable();
    }

    public async Task<Option<Notification>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .Include(n => n.User)
            .FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
    }

    public async Task<Either<Error, Notification>> SaveAsync(Notification entity, CancellationToken cancellationToken)
    {
        await _context.Notifications.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Notification>> UpdateAsync(Notification entity, CancellationToken cancellationToken)
    {
        _context.Notifications.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<Either<Error, Unit>> DeleteAsync(Notification entity, CancellationToken cancellationToken)
    {
        _context.Notifications.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Default;
    }
}