namespace PromoCodeFactory.DataAccess.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using PromoCodeFactory.Core.Abstractions.Repositories;
    using PromoCodeFactory.Core.Domain;

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

        public Task<Guid> CreateAsync(T entity)
        {
            entity.Id = Guid.NewGuid();
            Data = Data.Concat(new[] { entity });
            return Task.FromResult(entity.Id);
        }

        public Task<Guid> UpdateAsync(T entity)
        {
            CheckEntityExisted(entity.Id);

            var list = Data as List<T>;

            list.RemoveAll(x => x.Id == entity.Id);
            list.Add(entity);

            return Task.FromResult(entity.Id);
        }

        public void DeleteAsync(Guid id)
        {
            CheckEntityExisted(id);

            var list = Data as List<T>;

            list.RemoveAll(x => x.Id == id);
        }

        /// <summary>
        ///     Проверка на то, что изменяемая сущность существует
        /// </summary>
        private void CheckEntityExisted(Guid id)
        {
            var updateEntity = Data.FirstOrDefault(x => x.Id == id);

            if (updateEntity == default)
                throw new ArgumentException($"Entity on ID : {id} not found");
        }
    }
}