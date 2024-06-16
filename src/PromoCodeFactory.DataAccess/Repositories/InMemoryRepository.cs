using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
namespace PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>: IRepository<T> where T : BaseEntity
    {
        protected IEnumerable<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            Data = data;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task<Guid> CreateAsync(T entity)
        {
            var newEntity = entity as T;
            newEntity.Id = Guid.NewGuid(); // в идеале, это надо бы повесить на репозиторий
            Data = Data.Append(newEntity);
            return Task.FromResult(newEntity.Id);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            if (Data.Where(x => x.Id == id).Count() < 1)
            {
                return Task.FromResult(false);
            }
            var newData = Data.Where(x => x.Id != id);
            Data = newData;
            return Task.FromResult(true);
        }

        public Task<bool> UpdateAsync(T entity)
        {
            if (Data.Where(x => x.Id == entity.Id).Count() < 1)
            {
                return Task.FromResult(false);
            }
            var newData = Data.Where(x => x.Id != entity.Id); // immutable, поэтому удаляем старое и добавляем новое
            Data = newData.Append(entity);
            return Task.FromResult(true);
        }
    }
}