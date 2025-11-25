using Microsoft.EntityFrameworkCore;

namespace CarManagementSystem
{
    public class CarDbContext:DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext>options):base(options)
        {
            
        }

        public DbSet<Car>Cars { get; set; }//creating a table named Cars in the database
    }
}
