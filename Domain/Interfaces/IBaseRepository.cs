using LanguageExt;
using LanguageExt.Common;

namespace Domain.Interfaces;

public interface IBaseRepository<T>
{
    public IQueryable<T> GetAllEntities();
    public Task<Option<T>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<Either<Error, T>> SaveAsync(T entity, CancellationToken cancellationToken);
    public Task<Either<Error, T>> UpdateAsync(T entity, CancellationToken cancellationToken);
    public Task<Either<Error, Unit>> DeleteAsync(T entity, CancellationToken cancellationToken);
}