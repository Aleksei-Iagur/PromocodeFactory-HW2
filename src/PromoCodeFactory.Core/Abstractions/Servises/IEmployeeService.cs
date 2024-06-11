using PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PromoCodeFactory.Core.Abstractions.Servises
{
    /// <summary>
    ///     Сервис по работе с сотрудниками
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        ///     Получить сотрудника по его Id
        /// </summary>
        /// <param name="id">Id искомого сотрудника</param>
        Task<Employee> GetById(Guid id);

        /// <summary>
        ///     Получить всех сотрудников
        /// </summary>
        Task<List<Employee>> GetEmployeesAsync();

        /// <summary>
        ///     Добавление сотрудника в БД
        /// </summary>
        /// <param name="employeeForCreate">Сотрудник, которого нужно сохранить</param>
        Task<Guid> CreateEmployee(Employee employeeForCreate);

        /// <summary>
        ///     Удаление сотрудника по его Id
        /// </summary>
        /// <param name="employeeId">Id сотрудника, которого надо удалить</param>
        Task DeleteEmployee(Guid employeeId);

        /// <summary>
        ///     Обновление сотрудника
        /// </summary>
        /// <param name="id">Id сотрудника, которого надо обновнить</param>
        /// <param name="employeeForUpdate">Данные для обонвления</param>
        Task<Employee> UpdateEmployee(Guid id, Employee employeeForUpdate);
    }
}
