using Microsoft.EntityFrameworkCore;
using MinimalApiExercise.Data;
using MinimalApiExercise.DTOs;

namespace MinimalApiExercise.Endpoints;

public abstract class ProductEndpoints
{
    public static void RegisterEndpoints(WebApplication app)
    {
        // Get all products.
        app.MapGet("/products", async (StoreDbContext context) =>
        {
            return Results.Ok(await context.Products
                .Select(p => new ProductDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    ProductDescription = p.Description
                })
                .ToListAsync());
        });
        
        // Search for products.
        app.MapGet("products/search", async (StoreDbContext context, string? query) =>
        {
            var products = await context.Products
                .Select(p => new ProductDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    ProductDescription = p.Description
                })
                .ToListAsync();

            return string.IsNullOrEmpty(query) ? products : products.Where(p => p.ProductName
                .Contains(query, StringComparison.CurrentCultureIgnoreCase)).ToList();
        });
    }
}