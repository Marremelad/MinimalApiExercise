using MinimalApiExercise.Services;

namespace MinimalApiExercise.Endpoints;

public abstract class ProductEndpoints
{
    private const string InvalidOperationMessage = "Invalid operation status";
    
    public static void RegisterEndpoints(WebApplication app)
    {
        // Get all products.
        app.MapGet("/products", async (ProductService productService) =>
        {
            var (operationStatus, response) = await productService.GetAllProducts();

            return operationStatus switch
            {
                0 => Results.Ok(response),
                1 => Results.Problem(response.ToString(), statusCode: 500),
                _ => Results.BadRequest()
            };
        });
        
        // Get product.
        app.MapGet("/products/{id:int}", async (ProductService productService, int id) =>
        {
            var (operationStatus, response) = await productService.GetProduct(id);

            return operationStatus switch
            {
                0 => Results.Ok(response),
                1 => Results.Problem(response.ToString(), statusCode: 500),
                2 => Results.NotFound(response),
                _ => Results.BadRequest(InvalidOperationMessage)
            };
        });
        
        // Search for products.
        app.MapGet("products/search", async (ProductService productService, string? query) =>
        {
            var (operationStatus, response) = await productService.SearchProducts(query);

            return operationStatus switch
            {
                0 => Results.Ok(response),
                1 => Results.Problem(response.ToString(), statusCode: 500),
                _ => Results.BadRequest(InvalidOperationMessage)
            };
        });
        
        // Search for product by price range.
        app.MapGet("products/priceRange/search",
            async (ProductService productService, decimal? minPrice, decimal? maxPrice) =>
            {
                var (operationStatus, response) = await productService.SearchProductByPriceRange(minPrice, maxPrice);

                return operationStatus switch
                {
                    0 => Results.Ok(response),
                    1 => Results.Problem(response.ToString(), statusCode: 500),
                    _ => Results.BadRequest(InvalidOperationMessage)
                };
            });
    }
}