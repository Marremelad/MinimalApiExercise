using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MinimalApiExercise.Data;
using MinimalApiExercise.DTOs;
using MinimalApiExercise.DTOs.OrderDTOs;
using MinimalApiExercise.Models;

namespace MinimalApiExercise.Services;

public class OrderService(StoreDbContext context)
{
    private const string OperationsSuccessfulMessage = "Operation successful";
    private const string NotFoundMessage = "not found";
    private const string OrderTerminatedMessage = "Order terminated:";
    
    // Get all orders.
    public async Task<(int, object)> GetAllOrders()
    {
        List<OrderDto> orders;
        
        try
        {
            orders = await context.Orders
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
                .ToListAsync();
        }
        catch (Exception e)
        {
            return (1, e);
        }

        return (0, orders);
    }
    
    // Get order by ID.
    public async Task<(int, object)> GetOrder(int id)
    {
        OrderDto? order;
        try
        {
            order = await context.Orders
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

            if (order == null) return (2, "Order " + NotFoundMessage);
        }
        catch (Exception e)
        {
            return (1, e);
        }

        return (0, order);
    }
    
    // Create order.
    public async Task<(int, object)> CreateOrder(OrderCreateDto newOrder)
    {
        try
        {
            var validationContext = new ValidationContext(newOrder);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(newOrder, validationContext, validationResult, true);

            if (!isValid) return (3, validationResult.Select(v => v.ErrorMessage));

            var customer = await context.Customers
                .Where(c => c.Id == newOrder.CustomerId)
                .FirstOrDefaultAsync();

            if (customer == null) return (2, "Customer " + NotFoundMessage);

            var order = new Order
            {
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                CustomerIdFk = newOrder.CustomerId,
            };
        
            context.Orders.Add(order);
            await context.SaveChangesAsync();
        
            return await AddItemsToOrder(newOrder.Items, order);
        }
        catch (Exception e)
        {
            return (1, e.ToString());
        }
    }

    // Add items to order.
    private async Task<(int, string)> AddItemsToOrder(List<string> items, Order order)
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
            return (3, OrderTerminatedMessage + " No items were added");
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

        return (0, $"Order created successfully. Items added: " +
                   $"{string.Join(", ", addedItems.Select(ai => ai.Name))}." +
                   $"{(excludedItems.Count == 0 ? "" 
                       : $" Items with id: '{string.Join(", ", excludedItems)}': were not added because they do not exist.")}");

    }
}