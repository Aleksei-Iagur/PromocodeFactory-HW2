namespace PromoCodeFactory.WebHost.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using PromocodeFactory.Domain.Services.Interfaces;
    using PromoCodeFactory.Contracts.Models;
    using PromoCodeFactory.Core.Abstractions.Repositories;
    using PromoCodeFactory.Core.Domain.Administration;
    using PromoCodeFactory.WebHost.Models;

    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IEmployeeServices _employeeServices;

        public EmployeesController(IRepository<Employee> employeeRepository, IEmployeeServices employeeServices)
        {
            _employeeRepository = employeeRepository;
            _employeeServices = employeeServices;
        }

        /// <summary>
        ///     Получить данные всех сотрудников
        /// </summary>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();

            var employeesModelList = employees.Select(x =>
                new EmployeeShortResponse()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FullName,
                }).ToList();

            return employeesModelList;
        }

        /// <summary>
        ///     Получить данные сотрудника по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,
                Roles = employee.Roles.Select(x => new RoleItemResponse()
                {
                    Name = x.Name,
                    Description = x.Description
                }).ToList(),
                FullName = employee.FullName,
                AppliedPromocodesCount = employee.AppliedPromocodesCount
            };

            return employeeModel;
        }

        /// <summary>
        ///     Создание сотрудника в БД
        /// </summary>
        /// <param name="createEmployeeRequest">Реквест с данными на создание сотрудника в бд</param>
        [HttpPost("create")]
        public async Task<ActionResult<Guid>> CreateEmployeeAsync([FromBody] CreateEmployeeRequest createEmployeeRequest)
        {
            return await _employeeServices.CreateEmployee(createEmployeeRequest);
        }

        /// <summary>
        ///     Обновление даных сотрудника в БД
        /// </summary>
        /// <param name="updateEmployeeRequest">Реквест с обновленными данными сотрудника</param>
        [HttpPut("update")]
        public async Task<ActionResult<Guid>> CreateEmployeeAsync([FromBody] UpdateEmployeeRequest updateEmployeeRequest)
        {
            try
            {
                await _employeeServices.UpdateEmployee(updateEmployeeRequest);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        ///     Удаление данных сотрудника в БД
        /// </summary>
        [HttpDelete("delete/{id:guid}")]
        public ActionResult DeleteEmployeeAsync(Guid id)
        {
            try
            {
                _employeeServices.DeleteEmployee(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}