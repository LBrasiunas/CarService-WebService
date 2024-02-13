using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class CarUpdateDto
{
    [MinLength(2)]
    [MaxLength(20)]
    public string? Brand { get; set; }

    [MinLength(2)]
    [MaxLength(50)]
    public string? Model { get; set; }

    [Range(1800, 2100)]
    public int? Year { get; set; }
}
