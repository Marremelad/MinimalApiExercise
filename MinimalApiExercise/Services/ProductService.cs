using Microsoft.EntityFrameworkCore;
using MinimalApiExercise.Data;
using MinimalApiExercise.DTOs;

namespace MinimalApiExercise.Services;

public class ProductService(StoreDbContext context)
{
    private const string OperationsSuccessfulMessage = "Operation successful";
    private const string NotFoundMessage = "not found";
    
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

    // Get all products paginated.
    public async Task<(int, object)> GetAllProductsPaginated(int pageNumber = 1, int pageSize = 3)
    {
        int totalRecords;
        List<ProductDto> products;
        try
        {
            totalRecords = await context.Products.CountAsync();
            products = await context.Products
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
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

        return (0, new
        {
            Records = $"{pageSize} out of {totalRecords}",
            Products = products
        });
    }

    // Get all products ordered by ascending/descending price.
    public async Task<(int, object)> GetAllProductsAscendingDescending(bool ascending)
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

            if (!ascending)
            {
                products = products
                    .OrderByDescending(p => p.ProductPrice)
                    .ToList();
            }
        }
        catch (Exception e)
        {
            return (1, e);
        }

        return (0, products);
    }
}