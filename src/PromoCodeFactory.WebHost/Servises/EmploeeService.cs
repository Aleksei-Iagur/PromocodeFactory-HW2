using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Abstractions.Servises;
using PromoCodeFactory.Core.Domain.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodeFactory.WebHost.Servises
{
    /// <inheritdoc cref="IEmployeeService"/>
    public class EmploeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmploeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Guid> CreateEmployee(Employee employeeForCreate)
        {
            await _employeeRepository.Add(employeeForCreate);
            return employeeForCreate.Id;
        }

        public async Task DeleteEmployee(Guid employeeId)
        {
            await _employeeRepository.Delete(employeeId);
        }

        public async Task<Employee> GetById(Guid id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return (await _employeeRepository.GetAllAsync()).ToList();
        }

        public async Task<Employee> UpdateEmployee(Guid id, Employee employeeForUpdate)
        {
            var oldEmployee = await _employeeRepository.GetByIdAsync(id);
            if (oldEmployee == null)
                throw new Exception("Not Found");

            employeeForUpdate.Id = oldEmployee.Id;
            employeeForUpdate.Roles.AddRange(oldEmployee.Roles);

            employeeForUpdate.Roles = employeeForUpdate.Roles.DistinctBy(x => new { x.Name, x.Description }).ToList();

            await _employeeRepository.Update(id, employeeForUpdate);

            return employeeForUpdate;
        }
    }
}
