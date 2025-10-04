using System;
using Demo1.Models;

namespace Demo1.Services;

public interface IDemoRepository
{
    public Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    // public Task<Employee?> GetEmployeeByIdAsync(Guid id);
    public Task AddEmployeeAsync(Employee employee);
    public Task DeleteEmployeeAsync(Guid id);
}
