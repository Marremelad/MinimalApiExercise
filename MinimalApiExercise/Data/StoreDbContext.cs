using MinimalApiExercise.Models;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;

namespace MinimalApiExercise.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
