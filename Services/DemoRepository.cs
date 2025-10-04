using System;
using System.Formats.Asn1;
using Demo1.Data;
using Demo1.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo1.Services;

public class DemoRepository : IDemoRepository
{
    private readonly EmployeeDbContext _dbContext;
    public DemoRepository(EmployeeDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _dbContext.Employees.OrderBy(emp => emp.Name).ToListAsync();
    }

    public async Task AddEmployeeAsync(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }
        await _dbContext.Employees.AddAsync(employee);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteEmployeeAsync(Guid id)
    {
        var emp = await _dbContext.Employees.FindAsync(id);
        if (emp != null)
        {
            _dbContext.Employees.Remove(emp);
            await _dbContext.SaveChangesAsync();
        }
    }
}
