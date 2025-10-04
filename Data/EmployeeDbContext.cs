using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Demo1.Data;

public class EmployeeDbContext : DbContext
{

    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
    {
    }
    
    public DbSet<Models.Employee> Employees { get; set; }
}
