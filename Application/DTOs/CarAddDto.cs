using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class CarAddDto
{
    [MinLength(2)]
    [MaxLength(20)]
    public required string Brand { get; set; }

    [MinLength(2)]
    [MaxLength(50)]
    public required string Model { get; set; }

    [Range(1800, 2100)]
    public required int Year { get; set; }
}
