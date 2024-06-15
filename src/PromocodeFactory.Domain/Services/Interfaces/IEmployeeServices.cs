using PromoCodeFactory.Contracts.Models;

namespace PromocodeFactory.Domain.Services.Interfaces
{
    /// <summary>
    ///     Интерфейс для работы с данными о сотрудниках
    /// </summary>
    public interface IEmployeeServices
    {
        /// <summary>
        ///     Метод создания сотрудника
        /// </summary>
        public Task<Guid> CreateEmployee(CreateEmployeeRequest createEmployeeRequest);

        /// <summary>
        ///     Метод обновления данных сотрудника
        /// </summary>
        public Task<Guid> UpdateEmployee(UpdateEmployeeRequest createEmployeeRequest);

        /// <summary>
        ///     Метод удаления сотрудника из БД
        /// </summary>
        /// <param name="id"></param>
        public void DeleteEmployee(Guid id);
    }
}
