namespace PromoCodeFactory.Core.Abstractions.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PromoCodeFactory.Core.Domain;

    public interface IRepository<T>
        where T: BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);

        Task<T> Create(T entity);

        Task<T> Update(T entity);

        Task<bool> Delete(Guid id);
    }
}