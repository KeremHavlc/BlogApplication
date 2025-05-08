using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using DataAccess.Context;
using System.IO;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var environmentName = "Development";  // Geliştirme ortamını belirliyoruz
        var currentDirectory = Directory.GetCurrentDirectory();
        
        // WebApi'nin bulunduğu dizine göre appsettings.json yolunu doğru bir şekilde al
        var basePath = Path.Combine(currentDirectory, "../WebApi");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath) // WebApi projesinin dizini
            .AddJsonFile("appsettings.json") // WebApi'nin kök dizinindeki appsettings.json
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseSqlServer(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}
