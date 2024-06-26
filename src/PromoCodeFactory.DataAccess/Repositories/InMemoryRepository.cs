using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain;
namespace PromoCodeFactory.DataAccess.Repositories
{
    public class InMemoryRepository<T>: IRepository<T> where T: BaseEntity
    {
        private const string MESSAGE_ERROR = "Ошибка работы с БД";
        private const string NOT_FOUND_MESSAGE_ERROR = "Ошибка работы с БД";
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
        public Task<T> CreateAsync(T entity)
        {
            entity.Id = Guid.NewGuid();

            Data = Data.Append(entity);
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == entity.Id));
        }

        public Task<T> UpdateAsync(Guid id, T model)
        {
            var entity = Data.FirstOrDefault(x => x.Id == id);

            if (entity == null)
                throw new Exception(NOT_FOUND_MESSAGE_ERROR);

            Data = Data.Select(x => x.Id == entity.Id ? model : x);

            return Task.FromResult(Data.FirstOrDefault(x => x.Id == id));
        }

        public Task RemoveAsync(Guid id)
        {
            var entity = Data.FirstOrDefault(x => x.Id == id);


            if (entity == null)
                throw new Exception(NOT_FOUND_MESSAGE_ERROR);

            Data = Data.Where(x => x.Id != id).Select(x => x);

            return Task.CompletedTask;
        }
    }
}