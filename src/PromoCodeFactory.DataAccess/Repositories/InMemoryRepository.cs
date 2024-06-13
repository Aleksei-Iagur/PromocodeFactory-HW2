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

        public Task<T> CreateRecord(T rec)
        {
            var newList = Data.ToList();
            newList.Add(rec);
            Data = newList;
            return Task.FromResult(Data.FirstOrDefault(x => x.Id == rec.Id));
        }

        public Task<T> UpdateRecord(Guid id, T rec)
        {
            var newList = Data.ToList();
            var index = newList.FindIndex(x => x.Id == id);

            if (index != -1)
            {
                newList[index] = rec;
            }
            Data = newList;
            var curRec = Data.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(curRec);
        }

        public Task DeleteRecord(Guid id)
        {
            var newList = Data.ToList();
            newList.Remove(Data.FirstOrDefault(x => x.Id == id));
            Data = newList;

            return Task.CompletedTask;
        }
    }
}