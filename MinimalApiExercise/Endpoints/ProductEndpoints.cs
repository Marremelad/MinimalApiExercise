using Microsoft.AspNetCore.Http.HttpResults;
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
        app.MapGet("products/price-range/search",
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
        
        // Get all products paginated.
        app.MapGet("products/page/{pageNumber:int?}", async (ProductService productService, int pageNumber = 1) =>
        {
            var (operationStatus, response) = await productService.GetAllProductsPaginated(pageNumber);

            return operationStatus switch
            {
                0 => Results.Ok(response),
                1 => Results.Problem(response.ToString(), statusCode: 500),
                _ => Results.BadRequest(InvalidOperationMessage)
            };
        });
        
        // Get all products ordered by ascending/descending price.
        app.MapGet("products/oder-by-ascending{ascending:bool}", async (ProductService productService, bool ascending) =>
        {
            var (operationStatus, response) = await productService.GetAllProductsAscendingDescending(ascending);

            return operationStatus switch
            {
                0 => Results.Ok(response),
                1 => Results.Problem(response.ToString(), statusCode: 500),
                _ => Results.BadRequest(InvalidOperationMessage)
            };
        });
    }
}