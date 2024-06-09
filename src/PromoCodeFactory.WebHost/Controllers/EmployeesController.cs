using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromoCodeFactory.Core.Abstractions.Repositories;
using PromoCodeFactory.Core.Domain.Administration;
using PromoCodeFactory.WebHost.Models;

namespace PromoCodeFactory.WebHost.Controllers
{
    /// <summary>
    /// Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _employeeRepository;

        public EmployeesController(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Получить данные всех сотрудников
        /// </summary>
        /// <returns></returns>
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
        /// Получить данные сотрудника по Id
        /// </summary>
        /// <returns></returns>
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
        /// Создание нового сотрудника.
        /// </summary>
        /// <param name="model"> Модель создаваемого сотрудника. </param>
        /// <returns>
        /// <list type="table">
        /// <item><c>200</c> и модель созданного сотрудника. </item>
        /// <item><c>400</c> и сообщение об ошибке, в противном случае. </item>
        /// </list>
        /// </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> Create([FromBody] EmployeeRequest model)
        {
            if (model is null)
            {
                return this.BadRequest($"Parameter {nameof(model)} is null");
            }

            var newEntity = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                AppliedPromocodesCount = model.AppliedPromocodesCount,
                Roles = model.Roles.Select(s => new Role
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description
                }).ToList(),
            };

            var entity = await this._employeeRepository.Create(newEntity);

            var result = new EmployeeResponse
            {
                FullName = entity.FullName,
                Email = entity.Email,
                Roles = entity.Roles.Select(s => new RoleItemResponse
                {
                    Id = s.Id,
                    Description = s.Description,
                    Name = s.Name
                }).ToList(),
                AppliedPromocodesCount = entity.AppliedPromocodesCount,
                Id = entity.Id
            };

            return this.Ok(result);
        }

        /// <summary>
        /// Обновление информации о сотруднике.
        /// </summary>
        /// <param name="model"> Модель сотрудника с обновленными данными. </param>
        /// <returns>
        /// <list type="table">
        /// <item><c>200</c> и обновленная модель сотрудника. </item>
        /// <item><c>400> и сообщение об ошибке. </c></item>
        /// </list>
        /// </returns>
        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> Update([FromBody] EmployeeRequest model)
        {
            if (model is null)
            {
                return this.BadRequest($"Parameter {nameof(model)} is null");
            }

            var newEntity = new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                AppliedPromocodesCount = model.AppliedPromocodesCount,
                Roles = model.Roles.Select(s => new Role
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description
                }).ToList(),
            };

            var entity = await this._employeeRepository.Update(newEntity);

            var result = new EmployeeResponse
            {
                FullName = entity.FullName,
                Email = entity.Email,
                Roles = entity.Roles.Select(s => new RoleItemResponse
                {
                    Id = s.Id,
                    Description = s.Description,
                    Name = s.Name
                }).ToList(),
                AppliedPromocodesCount = entity.AppliedPromocodesCount,
                Id = entity.Id
            };

            return this.Ok(result);
        }

        /// <summary>
        /// Удаляет элемент по идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор сотрудника. </param>
        /// <returns>
        /// <list type="table">
        /// <item><c>200</c> и идентификатор удаленного сотрудника. </item>
        /// <item><c>404</c> и переданный идентификатор, если сотрудник не был найден. </item>
        /// </list>
        /// </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return await this._employeeRepository.Delete(id)
                ? this.Ok(id)
                : this.BadRequest(id);
        }
    }
}