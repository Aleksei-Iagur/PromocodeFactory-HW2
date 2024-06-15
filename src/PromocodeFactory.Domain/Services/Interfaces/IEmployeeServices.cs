using PromoCodeFactory.Contracts.Models;
using PromoCodeFactory.Core.Domain.Administration;

namespace PromocodeFactory.Domain.Services.Interfaces
{
    /// <summary>
    ///     Интерфейс для работы с данными о сотрудниках
    /// </summary>
    public interface IEmployeeServices
    {
        /// <summary>
        ///     Получить всех работников из БД
        /// </summary>
        public Task<IEnumerable<Employee>> GetEmployeesAsync();

        /// <summary>
        ///     Получить работника по Uuid
        /// </summary>
        /// <param name="guid">Идентификатор сотрудника</param>
        public Task<Employee> GetEmployeeByUuid(Guid guid);

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
