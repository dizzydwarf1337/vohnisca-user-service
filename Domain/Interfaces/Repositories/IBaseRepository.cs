using LanguageExt;
using LanguageExt.Common;

namespace Domain.Interfaces.Repositories;

public interface IBaseRepository<T>
{
    public Either<Error, IQueryable<T>> GetAllEntities();
    public Task<Either<Error, T>> GetByIdAsync(Guid id);
    public Task<Either<Error, T>> SaveAsync(T entity);
    public Task<Either<Error, T>> UpdateAsync(T entity);
    public Task<Either<Error, T>> DeleteAsync(T entity);
}