
using HelloWorld.Models;
using Microsoft.EntityFrameworkCore;

namespace HelloWorld.Data;

// Inheritance: DataContextEF inherits from DbContext,
// allowing it to use accessible members and extend its functionality.
// Abstraction: DbContext, Hiding implementation details and exposing essential functionality
public class DataContextEF : DbContext
{
    // DbSet represents a collection of Computer entities
    // that EF Core maps to a database table.
    public DbSet<Computer>? Computer { get; set; }

    // Connection string contains the information required
    // to connect to the SQL Server database.
    // Encapsulation: Controlling access to data and implementation
    private string _connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true";

    // Polymorphism: Method overriding allows DataContextEF
    // to provide its own implementation of OnConfiguring().
    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        // Check whether the DbContext has already been configured.
        if (!optionsBuilder.IsConfigured)
        {
            // Configure SQL Server and enable automatic retries
            // for transient connection failures.
            optionsBuilder.UseSqlServer(
                _connectionString,
                optionsBuilder => optionsBuilder.EnableRetryOnFailure()
            );
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Override OnModelCreating() to configure the database model
        // and define how entity classes map to database tables.

        // Set the default database schema.
        // Entities without an explicitly configured schema will use this schema.
        modelBuilder.HasDefaultSchema("Project1AppSchema");

        // Configure the Computer entity using Fluent API.
        modelBuilder.Entity<Computer>()

            // Set ComputerId as the primary key of the Computer entity.
            .HasKey(c => c.ComputerId);

        // Configure the entity as keyless (without a primary key).
        // Useful for read-only query results and database views.
        // .HasNoKey();

        // Explicitly configure the database table name and schema.
        // First argument: Table name.
        // Second argument: Schema name.
        // .ToTable("Computer", "Project1AppSchema");

        // Another example of explicitly specifying a table and schema.
        // .ToTable("Computer", "SchemaName");
    }

}
