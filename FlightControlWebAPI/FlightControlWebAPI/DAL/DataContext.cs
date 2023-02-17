using FlightControlWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightControlWebAPI.DAL
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Terminal> Terminals { get; set; }
        public virtual DbSet<Logger> Logs { get; set; }

        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public override int SaveChanges()
        {
            semaphoreSlim.Wait();
            try
            {
                return base.SaveChanges();
            }
            finally 
            { 
                semaphoreSlim.Release(); 
            }
            
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
        {
            await semaphoreSlim.WaitAsync(cancellationToken);
            try
            {
                return await base.SaveChangesAsync();
            }
            finally 
            {
                semaphoreSlim.Release(); 
            }
        }
    }
}
