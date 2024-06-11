namespace PromoCodeFactory.WebHost.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using PromoCodeFactory.Core.Abstractions.Repositories;
    using PromoCodeFactory.Core.Domain.Administration;
    using PromoCodeFactory.WebHost.Models;
    using PromoCodeFactory.WebHost.Models.ModelsIn;
    using PromoCodeFactory.Core.Abstractions.Servises;

    /// <summary>
    ///     Сотрудники
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeesController(IMapper mapper, IEmployeeService employeeService)
        {
            _mapper = mapper;
            _employeeService = employeeService;
        }

        /// <summary>
        ///     Получить данные всех сотрудников
        /// </summary>
        [HttpGet]
        public async Task<List<EmployeeShortResponse>> GetEmployeesAsync()
        {
            var employees = await _employeeService.GetEmployeesAsync();

            var employeesModelList = _mapper.Map<List<EmployeeShortResponse>>(employees);

            return employeesModelList;
        }

        /// <summary>
        ///     Получить данные сотрудника по Id
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmployeeResponse>> GetEmployeeByIdAsync(Guid id)
        {
            var employee = await _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            var employeeModel = _mapper.Map<EmployeeResponse>(employee);

            return employeeModel;
        }

        /// <summary>
        ///    Добавить нового сотрудника 
        /// </summary>
        [HttpPost("createEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeeForCreateDto employForCreate)
        {
            var employee = _mapper.Map<Employee>(employForCreate);

            var id = await _employeeService.CreateEmployee(employee);

            return Ok(id);
        }

        /// <summary>
        ///    Обновить сотрудника
        /// </summary>
        [HttpPut("UpdateEmployee/{id:guid}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeForCreateDto employForCreate)
        {
            try
            {
                var employee = _mapper.Map<Employee>(employForCreate);

                await _employeeService.UpdateEmployee(id, employee);

                return Ok(employee.Id);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        /// <summary>
        ///    Удалить сотрудника
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> RemoveEmployee(Guid id)
        {
            try
            {
                await _employeeService.DeleteEmployee(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}