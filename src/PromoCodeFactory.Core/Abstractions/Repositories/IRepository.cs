namespace PromoCodeFactory.Core.Abstractions.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using PromoCodeFactory.Core.Domain;

    public interface IRepository<T> where T: BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);

        Task<Guid> CreateAsync(T entity);

        Task<Guid> UpdateAsync(T entity);

        void DeleteAsync(Guid id);
    }
}