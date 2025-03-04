using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MinimalApiExercise.Data;
using MinimalApiExercise.DTOs;
using MinimalApiExercise.DTOs.OrderDTOs;
using MinimalApiExercise.Models;

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
                OrderedProducts = o.OrderProducts!
                    .Select(op => new ProductDto
                    {
                        ProductId = op.Product!.Id,
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
                OrderedProducts = o.OrderProducts!
                    .Select(op => new ProductDto
                    {
                        ProductId = op.Product!.Id,
                        ProductName = op.Product.Name,
                        ProductDescription = op.Product.Description
                    })
            }).FirstOrDefaultAsync();

        return order == null ? Results.NotFound("Order does not exist") : Results.Ok(order);
    }
    
    // Create order.
    public async Task<IResult> CreateOrder(OrderCreateDto newOrder)
    {
        var validationContext = new ValidationContext(newOrder);
        var validationResult = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(newOrder, validationContext, validationResult, true);
    
        if (!isValid) return Results.BadRequest(validationResult.Select(v => v.ErrorMessage));

        var customer = await context.Customers
            .Where(c => c.Id == newOrder.CustomerId)
            .FirstOrDefaultAsync();

        if (customer == null) return Results.NotFound("Customer does not exist");

        var order = new Order
        {
            OrderDate = DateOnly.FromDateTime(DateTime.Now),
            CustomerIdFk = newOrder.CustomerId,
        };
        
        context.Orders.Add(order);
        await context.SaveChangesAsync();
        
        return await AddItemsToOrder(newOrder.Items, order);

    }

    // Add items to order.
    private async Task<IResult> AddItemsToOrder(List<string> items, Order order)
    {
        var addedItems = new List<Product>();
        var excludedItems = new List<string>();
        foreach (var item in items)
        {
            if (int.TryParse(item, out var productId))
            {
                var itemToAdd = await context.Products.SingleOrDefaultAsync(p => p.Id == productId);

                if (itemToAdd != null)
                {
                    addedItems.Add(itemToAdd);
                }
                else
                {
                    excludedItems.Add(item);
                }
            }
        }

        if (addedItems.Count == 0)
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
            return Results.BadRequest("Order terminated: No items were added");
        }
            
        foreach (var orderProduct in addedItems.Select(item => new OrderProduct
                 {
                     OrderIdFk = order.Id,
                     ProductIdFk = item.Id
                 }))
        {
            context.OrderProducts.Add(orderProduct);
            await context.SaveChangesAsync();
        }

        return Results.Ok($"Order created successfully. Items added: {string.Join(", ", addedItems.Select(ai => ai.Name))}." +
                          $"{(excludedItems.Count == 0 ? "" : $" Items with id: '{string.Join(", ", excludedItems)}': " +
                                                              $"were not added because they do not exist.")}");

    }
}