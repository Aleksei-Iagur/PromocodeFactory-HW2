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

        public Task<T> CreateAsync(T model)
        {
            model.Id = Guid.NewGuid();

            Data = Data.Append(model);
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == model.Id));
        }

        public Task<T> UpdateAsync(T model)
        {
            var entity = Data.FirstOrDefault(x => x.Id == model.Id);

            Data = Data.Select(x => x.Id == entity.Id ? model : x);

            return Task.FromResult(Data.FirstOrDefault(x => x.Id == model.Id));
        }

        public Task DeleteAsync(Guid id)
        {      
            Data = Data.Where(x => x.Id != id).Select(x => x);

            return Task.CompletedTask;
        }
    }
}