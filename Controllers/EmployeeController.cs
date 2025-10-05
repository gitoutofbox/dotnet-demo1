using System.Collections;
using System.Threading.Tasks;
using Asp.Versioning;
using AutoMapper;
using Demo1.Data;
using Demo1.Models;
using Demo1.Models.DTOs;
using Demo1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo1.Controllers
{
    [Route("api/v{veresion:apiVersion}/employee")]
    [ApiController]
    [ApiVersion("1.0")]
    public class EmployeeController : ControllerBase
    {
        // private readonly EmployeeDbContext _dbContext;
        private readonly IDemoRepository _repository;
        private readonly IMapper _mapper;
        public EmployeeController(
            // EmployeeDbContext dbContext,
            IMapper mapper, IDemoRepository repository)
        {
            // _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployee()
        {
            // var employeeList = _dbContext.Employees.ToList();
            // return Ok(employeeList);
            var employeeList = await _repository.GetAllEmployeesAsync();
            return Ok(_mapper.Map<IEnumerable<EmployeeDTO>>(employeeList));
        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeeAddDTO payload)
        {
            // var emp = _mapper.Map<Employee>(payload);
            // // var emp = new Employee
            // // {
            // //     Name = payload.Name,
            // //     Position = payload.Position,
            // //     Salary = payload.Salary
            // // };
            // _dbContext.Employees.Add(emp);
            // _dbContext.SaveChanges();
            // return CreatedAtAction(nameof(GetEmployee), new { id = emp.Id }, emp);

            var emp = _mapper.Map<Employee>(payload);
            _repository.AddEmployeeAsync(emp);
            return CreatedAtAction(nameof(GetEmployee), new { id = emp.Id }, emp);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            // var emp = _dbContext.Employees.Find(id);
            // if (emp == null)
            // {
            //     return NotFound(new { Message = $"Employee with ID {id} not found." });
            // }

            // _dbContext.Employees.Remove(emp);
            // _dbContext.SaveChanges();
            // return NoContent();
            _repository.DeleteEmployeeAsync(id);
            return NoContent();
        }

    }
}
