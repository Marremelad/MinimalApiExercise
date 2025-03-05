using Microsoft.EntityFrameworkCore;
using MinimalApiExercise.Data;
using MinimalApiExercise.DTOs;

namespace MinimalApiExercise.Services;

public class ProductService(StoreDbContext context)
{
    private const string OperationsSuccessfulMessage = "Operation successful";
    private const string NotFoundMessage = "not found";
    private const string OrderTerminatedMessage = "Order terminated:";
    
    // Get all products.
    public async Task<(int, object)> GetAllProducts()
    {
        List<ProductDto> products;
        try
        {
            products = await context.Products
                .Select(p => new ProductDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    ProductDescription = p.Description,
                    ProductPrice = p.Price
                })
                .ToListAsync();
        }
        catch (Exception e)
        {
            return (1, e);
        }

        return (0, products);
    }
    
    // Get product.
    public async Task<(int, object)> GetProduct(int id)
    {
        ProductDto? product;
        try
        {
            product = await context.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    ProductDescription = p.Description,
                    ProductPrice = p.Price
                })
                .FirstOrDefaultAsync();

            if (product == null) return (2, "Product " + NotFoundMessage);
        }
        catch (Exception e)
        {
            return (1, e);
        }

        return (0, product);
    }
    
    // Search products.
    public async Task<(int, object)> SearchProducts(string? query)
    {
        List<ProductDto> products;
        try
        {
            products = await context.Products
                .Select(p => new ProductDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    ProductDescription = p.Description,
                    ProductPrice = p.Price
                })
                .ToListAsync();
        }
        catch (Exception e)
        {
            return (1, e);
        }
        
        return string.IsNullOrEmpty(query)
            ? (0, products)
            : (0, products.Where(p => p.ProductName
                .Contains(query, StringComparison.CurrentCultureIgnoreCase)).ToList());
    }
    
    // Search products by price range.
    public async Task<(int, object)> SearchProductByPriceRange(decimal? minPrice, decimal? maxPrice)
    {
        List<ProductDto> products;
        try
        {
            products = await context.Products
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
        }
        catch (Exception e)
        {
            return (1, e);
        }

        return (0, products);
    }
}