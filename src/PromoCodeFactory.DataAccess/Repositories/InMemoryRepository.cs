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

        public Task CreateAsync(T entity)
        {   
            Data = Data.Append(entity);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Guid id, T entity)
        {
            var model = GetByIdAsync(id).Result ?? throw new ArgumentNullException();
            foreach(var prop in model.GetType().GetProperties().Where(x => x.CanWrite))
            {
                var propDto = entity.GetType().GetProperty(prop.Name);
                var propDtoValue = propDto.GetValue(entity);
                prop.SetValue(model, propDtoValue);
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var model = GetByIdAsync(id).Result ?? throw new ArgumentNullException();
            var tempData = Data.ToList();
            tempData.Remove(model);
            Data = tempData;
            return Task.CompletedTask;
        }
    }
}