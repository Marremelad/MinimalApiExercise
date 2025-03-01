
using Microsoft.EntityFrameworkCore;
using MinimalApiExercise.Data;
using MinimalApiExercise.Models;

namespace MinimalApiExercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/users", (StoreDbContext context) =>
            {
                return Results.Ok(context.Customers.ToList());
            });

            app.MapPost("/users", (StoreDbContext context, Customer customer) =>
            {
                context.Customers.Add(customer);
                context.SaveChanges();
                return Results.Created($"/users/{customer.Id}", customer);
            });

            app.MapGet("/products", (StoreDbContext context) =>
            {
                return Results.Ok(context.Products.ToList());
            });

            app.MapPost("/products", (StoreDbContext context, Product product) =>
            {
                context.Products.Add(product);
                context.SaveChanges();
                return Results.Created($"/products/{product.Id}", product);
            });

            app.MapGet("/orders", (StoreDbContext context) =>
            {
                return Results.Ok(context.Orders.ToList());
            });

            app.MapPost("/orders", (StoreDbContext context, Order order) =>
            {
                context.Orders.Add(order);
                context.SaveChanges();
                return Results.Created($"/orders/{order.Id}", order);
            });

            app.Run();
        }
    }
}
