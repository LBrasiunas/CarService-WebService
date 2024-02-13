using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Database.Models;

public class Service
{
    [Key]
    public required int Id { get; set; }

    [MaxLength(50)]
    public required string Name { get; set; }

    [MaxLength(100)]
    public string? Description { get; set; }
}
