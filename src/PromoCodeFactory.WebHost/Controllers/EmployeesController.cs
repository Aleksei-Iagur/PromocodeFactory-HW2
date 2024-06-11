using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            var roles = employee.Roles ?? Enumerable.Empty<Role>();

            var employeeModel = new EmployeeResponse()
            {
                Id = employee.Id,
                Email = employee.Email,
                Roles = roles.Select(x => new RoleItemResponse()
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
        /// Создать сотрудника
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] EmployeeCreateDto dto)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                AppliedPromocodesCount = dto.AppliedPromocodesCount,
            };

            await _employeeRepository.CreateAsync(employee);
            return Created();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] EmployeeCreateDto dto)
        {
            var employee = new Employee()
            {
                Id = id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                AppliedPromocodesCount = dto.AppliedPromocodesCount,
            };

            try
            {
                await _employeeRepository.UpdateAsync(id, employee);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _employeeRepository.DeleteAsync(id);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }


    }
}