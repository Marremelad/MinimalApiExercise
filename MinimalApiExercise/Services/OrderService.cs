using Microsoft.EntityFrameworkCore;
using MinimalApiExercise.Data;
using MinimalApiExercise.DTOs;

namespace MinimalApiExercise.Services;

public class OrderService(StoreDbContext context)
{
    // Get all orders.
    public async Task<IResult> GetAllOrders()
    {
        return Results.Ok(await context.Orders
            .Select(o => new OrderDto
            {
                OrderId = o.Id,
                CustomerId = o.CustomerIdFk,
                OrderDate = o.OrderDate,
                OrderedProducts = o.OrderProducts
                    .Select(op => new ProductDto
                    {
                        ProductId = op.Product.Id,
                        ProductName = op.Product.Name,
                        ProductDescription = op.Product.Description
                    })
            })
            .ToListAsync());
    }
    
    // Get order by ID.
    public async Task<IResult> GetOrder(int id)
    {
        var order = await context.Orders
            .Where(o => o.Id == id)
            .Select(o => new OrderDto
            {
                OrderId = o.Id,
                CustomerId = o.CustomerIdFk,
                OrderDate = o.OrderDate,
                OrderedProducts = o.OrderProducts
                    .Select(op => new ProductDto
                    {
                        ProductId = op.Product.Id,
                        ProductName = op.Product.Name,
                        ProductDescription = op.Product.Description
                    })
            }).FirstOrDefaultAsync();

        return order == null ? Results.NotFound("Order does not exist") : Results.Ok(order);
    }
}