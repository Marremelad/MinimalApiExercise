using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MinimalApiExercise.Data;
using MinimalApiExercise.DTOs;
using MinimalApiExercise.DTOs.CustomerDTOs;
using MinimalApiExercise.Models;

namespace MinimalApiExercise.Endpoints;

public abstract class CustomerEndpoints
{
    public static void RegisterEndpoints(WebApplication app)
    {
        // Get all customers.
        app.MapGet("/customers", async (StoreDbContext context) =>
        {
            return Results.Ok(await context.Customers
                .Select(c => new CustomerDto
                {
                    CustomerId = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Phone = c.Phone

                })
                .ToListAsync());
        });

        // Get a specified customers' information.
        app.MapGet("/customers/{id:int}", async (StoreDbContext context, int id) =>
        {
            var customer = await context.Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Phone = c.Phone
                })
                .FirstOrDefaultAsync();

            return customer == null ? Results.NotFound("Customer does not exist.") : Results.Ok(customer);
        });
        
        // Get all the orders of a specified customer.
        app.MapGet("/customers/{id:int}/orders", async (StoreDbContext context, int id) =>
        {
            var customer = await context.Customers
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    CustomerName = $"{c.FirstName} {c.LastName}",
                    Orders = c.Orders!.Select(o => new OrderDto
                    {
                        OrderId = o.Id,
                        OrderedProducts = o.OrderProducts
                            .Select(op => new ProductDto
                            {
                                ProductId = op.Product.Id,
                                ProductName = op.Product.Name,
                                ProductDescription = op.Product.Description
                            })
                    })
                })
                .FirstOrDefaultAsync();

            return customer == null ? Results.NotFound("Customer does not exist.") : Results.Ok(customer);
        });
        
        // Adds a new customer to the database.
        app.MapPost("/customers/create", async (StoreDbContext context, CustomerCreateDto newCustomer) =>
        {
            var validationContext = new ValidationContext(newCustomer);
            var validationResult = new List<ValidationResult>();
            
            var isValid = Validator.TryValidateObject(newCustomer, validationContext, validationResult, true);
            
            if (!isValid) return Results.BadRequest(validationResult.Select(v => v.ErrorMessage));

            var customer = new Customer
            {
                FirstName = newCustomer.FirstName,
                LastName = newCustomer.LastName,
                Email = newCustomer.Email,
                Phone = newCustomer.Phone
            };

            context.Customers.Add(customer);
            await context.SaveChangesAsync();

            return Results.Ok(customer);
        });

        // Deletes a customer from the database.
        app.MapDelete("/customers/delete/{id:int}", async (StoreDbContext context, int id) =>
        {
            var customer = context.Customers
                .FirstOrDefault(c => c.Id == id);

            if (customer == null) return Results.NotFound("Customer does not exist.");

            context.Customers.Remove(customer);
            await context.SaveChangesAsync();

            return Results.Ok("Customer deleted successfully");
        });
    }
}