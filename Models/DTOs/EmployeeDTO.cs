using System;

namespace Demo1.Models.DTOs;

public class EmployeeDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Position { get; set; }= String.Empty;
    public decimal Salary { get; set; }
}
