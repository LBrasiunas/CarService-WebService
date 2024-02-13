using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Models;

public class Car
{
    [Key]
    public required int Id { get; set; }

    [MaxLength(20)]
    public required string Brand { get; set; }

    [MaxLength(50)]
    public required string Model { get; set; }

    [Range(1800, 2100)]
    public required int Year { get; set; }
}
