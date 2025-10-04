using System;
using System.ComponentModel.DataAnnotations;

namespace Demo1.Models.DTOs;

public class EmployeeAddDTO
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public string Name { get; set; } = String.Empty;

    [Required(ErrorMessage = "Position is required")]
    [MaxLength(50, ErrorMessage = "Position cannot exceed 50 characters")]
    public string Position { get; set; } = String.Empty;

    [Required(ErrorMessage = "Salary is required")]
    public decimal Salary { get; set; }
}

