using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public required DbSet<Car> Cars { get; set; }

    public required DbSet<Service> Services { get; set; }

    public required DbSet<CarAssignedToService> CarsAssignedToServices { get; set; }
}
