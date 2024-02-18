using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class ServiceAddDto
{
    [MinLength(2)]
    [MaxLength(50)]
    public required string Name { get; set; }

    [MinLength(2)]
    [MaxLength(100)]
    public string? Description { get; set; }
}
