using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class ServiceUpdateDto
{
    [MinLength(2)]
    [MaxLength(50)]
    public string? Name { get; set; }

    [MinLength(2)]
    [MaxLength(100)]
    public string? Description { get; set; }
}
