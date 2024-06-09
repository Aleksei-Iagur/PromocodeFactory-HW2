namespace PromoCodeFactory.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using PromoCodeFactory.Core.Abstractions.Repositories;
    using PromoCodeFactory.Core.Domain;

    public class InMemoryRepository<T>: IRepository<T>
        where T: BaseEntity
    {
        protected IEnumerable<T> Data { get; set; }

        public InMemoryRepository(IEnumerable<T> data)
        {
            this.Data = data;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(this.Data);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Task.FromResult(this.Data.FirstOrDefault(x => x.Id == id));
        }

        public Task<T> Create(T entity)
        {
            var data = this.Data.ToList();

            entity.Id = Guid.NewGuid();

            data.Add(entity);

            this.Data = data;

            return Task.FromResult(entity);
        }

        public async Task<T> Update(T entity)
        {
            var oldEntity = await this.GetByIdAsync(entity.Id);

            if (oldEntity is null)
            {
                return null;
            }

            var data = this.Data.ToList();

            data.Remove(oldEntity);

            data.Add(entity);

            this.Data = data;

            return await Task.FromResult(entity);
        }

        public async Task<bool> Delete(Guid id)
        {
            var element = await this.GetByIdAsync(id);

            if (element is null)
            {
                return false;
            }

            var data = this.Data.ToList();

            data.Remove(element);

            this.Data = data;

            return true;
        }
    }
}