using Microsoft.EntityFrameworkCore;
using MinimalApiExercise.Data;
using MinimalApiExercise.DTOs;

namespace MinimalApiExercise.Endpoints;

public class CustomerEndpoints
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
                    LastName = c.LastName
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
    }
}