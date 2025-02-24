using Microsoft.EntityFrameworkCore;
using MinimalApiExercise.Models;

namespace MinimalApiExercise.Data;

public class MinimalApiExerciseContext : DbContext
{
    public MinimalApiExerciseContext(DbContextOptions<MinimalApiExerciseContext> options) : base(options)
    {
        
    }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Service> Services { get; set; }
}