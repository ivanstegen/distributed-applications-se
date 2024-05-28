using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RestaurantReservationAPI.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RestaurantContext>
    {
        public RestaurantContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<RestaurantContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");

            builder.UseSqlServer(connectionString);

            return new RestaurantContext(builder.Options);
        }
    }
}
