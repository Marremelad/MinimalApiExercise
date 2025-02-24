using Microsoft.EntityFrameworkCore;
using MinimalApiExercise.Data;
using MinimalApiExercise.Models;

namespace MinimalApiExercise;

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

        builder.Services.AddDbContext<MinimalApiExerciseContext>(options =>
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

        // Fetches all customers from the database.
        app.MapGet("/customers", (MinimalApiExerciseContext context) => context.Customers);

        // Adds a customer to the database.
        app.MapPost("/customers", (MinimalApiExerciseContext context ,Customer customer) =>
        {
            context.Customers.Add(customer);
            context.SaveChanges();

            return customer;
        });

        // Updates the information of a customer in the database.
        app.MapPut("/customers/{id:int}", (MinimalApiExerciseContext context, int id, Customer customer) =>
        {
            var existingCustomer = context.Customers.FirstOrDefault(c => c.CustomerId == id);

            if (existingCustomer == null) return Results.NotFound("Customer not found");
            
            existingCustomer.CustomerName = customer.CustomerName;
            existingCustomer.CustomerEmail = customer.CustomerEmail;
            existingCustomer.CustomerPhoneNumber = customer.CustomerPhoneNumber;
            context.SaveChanges();
            
            return Results.Ok("Operation successful");
        });
        
        // Deletes a customer from the database.
        app.MapDelete("/customers/{id:int}", (MinimalApiExerciseContext context, int id) =>
        {
            var customer = context.Customers.FirstOrDefault(c => c.CustomerId == id);

            if (customer == null) return Results.NotFound("Customer not found");
            
            context.Customers.Remove(customer);
            context.SaveChanges();

            return Results.Ok("Operation successful");

        });

        // Gets all services from the database.
        app.MapGet("/services", (MinimalApiExerciseContext context) => context.Services);

        // Adds a service to the database.
        app.MapPost("/services", (MinimalApiExerciseContext context, Service service) =>
        {
            context.Services.Add(service);
            context.SaveChanges();

            return service;
        });
        
        // Updates the information of a service in the database.
        app.MapPut("/services/{id:int}", (MinimalApiExerciseContext context, int id, Service service) =>
        {
            var existingService = context.Services.FirstOrDefault(s => s.ServiceId == id);
        
            if (existingService == null) return Results.NotFound("Service not found");
        
            existingService.ServiceName = service.ServiceName;
            existingService.ServiceDescription = service.ServiceDescription;
            existingService.ServicePrice = service.ServicePrice;
            context.SaveChanges();
        
            return Results.Ok("Operation successful");
        });
        
        // Deletes a service from the database.
        app.MapDelete("/services/{id:int}", (MinimalApiExerciseContext context, int id) =>
        {
            var service = context.Services.FirstOrDefault(s => s.ServiceId == id);
        
            if (service == null) return Results.NotFound("Service not found");
            
            context.Services.Remove(service);
            context.SaveChanges();
        
            return Results.Ok("Operation successful");
        });
        
        app.Run();
    }
}