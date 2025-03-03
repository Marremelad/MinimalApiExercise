using MinimalApiExercise.Services;

namespace MinimalApiExercise.Endpoints;

public abstract class ProductEndpoints
{
    public static void RegisterEndpoints(WebApplication app)
    {
        // Get all products.
        app.MapGet("/products", async (ProductService productService) => 
            await productService.GetAllProducts()); 
        
        // Get product.
        app.MapGet("/products/{id:int}", async (ProductService productService, int id) => 
            await productService.GetProduct(id));
        
        // Search for products.
        app.MapGet("products/search", async (ProductService productService, string? query) => 
            await productService.SearchProducts(query));
        
        // Search for product by price range.
        app.MapGet("products/priceRange/search", async (ProductService productService, decimal? minPrice, decimal? maxPrice) => 
            await productService.SearchProductByPriceRange(minPrice, maxPrice));
    }
}