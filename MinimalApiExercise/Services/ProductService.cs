using Microsoft.EntityFrameworkCore;
using MinimalApiExercise.Data;
using MinimalApiExercise.DTOs;

namespace MinimalApiExercise.Services;

public class ProductService(StoreDbContext context)
{
    // Get all products.
    public async Task<IResult> GetAllProducts()
    {
        return Results.Ok(await context.Products
            .Select(p => new ProductDto
            {
                ProductId = p.Id,
                ProductName = p.Name,
                ProductDescription = p.Description,
                ProductPrice = p.Price
            })
            .ToListAsync());
    }
    
    // Get product.
    public async Task<IResult> GetProduct(int id)
    {
        var product = await context.Products
            .Where(p => p.Id == id)
            .Select(p => new ProductDto
            {
                ProductId = p.Id,
                ProductName = p.Name,
                ProductDescription = p.Description,
                ProductPrice = p.Price
            })
            .FirstOrDefaultAsync();

        return product == null ? Results.NotFound("Product does not exist") : Results.Ok(product);
    }
    
    // Search products.
    public async Task<IResult> SearchProducts(string? query)
    {
        var products = await context.Products
            .Select(p => new ProductDto
            {
                ProductId = p.Id,
                ProductName = p.Name,
                ProductDescription = p.Description,
                ProductPrice = p.Price
            })
            .ToListAsync();

        return string.IsNullOrEmpty(query)
            ? Results.Ok(products)
            : Results.Ok(products.Where(p => p.ProductName
                .Contains(query, StringComparison.CurrentCultureIgnoreCase)).ToList());
    }
    
    // Search products by price range.
    public async Task<IResult> SearchProductByPriceRange(decimal? minPrice, decimal? maxPrice)
    {
        var products = await context.Products
            .Where(p => 
                (!minPrice.HasValue || p.Price >= minPrice.Value) && 
                (!maxPrice.HasValue || p.Price <= maxPrice.Value)
            )
            .Select(p => new ProductDto
            {
                ProductId = p.Id,
                ProductName = p.Name,
                ProductDescription = p.Description,
                ProductPrice = p.Price
            })
            .ToListAsync();

        return Results.Ok(products);
    }
}