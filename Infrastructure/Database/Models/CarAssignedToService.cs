using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Models;

[PrimaryKey(nameof(ServiceId), nameof(CarId))]
public class CarAssignedToService
{
    public required int ServiceId { get; set; }

    public required int CarId { get; set; }
}
