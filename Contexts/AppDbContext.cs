using Microsoft.EntityFrameworkCore;
using TodoList.EntityConfigs;
using TodoList.Models;

namespace TodoList.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Todo> Todos => Set<Todo>();

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

        builder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
        );
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new TodoEntityConfig());
    }
}