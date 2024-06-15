namespace PromocodeFactory.Domain.Services.Implementation
{
    using PromocodeFactory.Domain.Converters;
    using PromocodeFactory.Domain.Services.Interfaces;
    using PromoCodeFactory.Contracts.Models;
    using PromoCodeFactory.Core.Abstractions.Repositories;
    using PromoCodeFactory.Core.Domain.Administration;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <inheritdoc cref="IEmployeeServices" />
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Role> _rolesRepository;

        public EmployeeServices(IRepository<Employee> employeeRepository, IRepository<Role> rolesRepository)
        {
            _employeeRepository = employeeRepository;
            _rolesRepository = rolesRepository;
        }

        public async Task<Guid> CreateEmployee(CreateEmployeeRequest createEmployeeRequest)
        {
            var newEmployee = EmployeeConverter.CreatreConvert(createEmployeeRequest);

            
            var includedRoles = await GetRolesForEmployeeAsync(createEmployeeRequest.RolesGuid);

            newEmployee.Roles = includedRoles.Any() ? includedRoles : null;

            return await _employeeRepository.CreateAsync(newEmployee);
        }

        public async Task<Guid> UpdateEmployee(UpdateEmployeeRequest updateEmployeeRequest)
        {
            var updatedEmployee = EmployeeConverter.UpdateConvert(updateEmployeeRequest);
            var includedRoles = await GetRolesForEmployeeAsync(updateEmployeeRequest.RolesGuid);

            updatedEmployee.Roles  =  includedRoles.Any() ? includedRoles : null;

            return await _employeeRepository.UpdateAsync(updatedEmployee);
        }

        public void DeleteEmployee(Guid id)
        {
            _employeeRepository.DeleteAsync(id);
        }

        /// <summary>
        ///   Метод для получения сущностей Роль по айди
        ///   Считаем, что при запросе создания работника, у нас уже есть все роли в БД
        /// <summary>
        private async Task<List<Role>> GetRolesForEmployeeAsync(List<Guid> rolesId)
        {
            var allRoles = await _rolesRepository.GetAllAsync();
            var includedRoles = allRoles.Where(x => rolesId.Contains(x.Id)).ToList();

            return includedRoles;
        }
    }
}
