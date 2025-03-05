using MinimalApiExercise.DTOs.OrderDTOs;
using MinimalApiExercise.Services;

namespace MinimalApiExercise.Endpoints;

public class OrderEndpoints
{
    private const string InvalidOperationMessage = "Invalid operation status";
    
    public static void RegisterEndpoints(WebApplication app)
    {
        // Get all orders.
        app.MapGet("/orders", async (OrderService orderService) =>
        {
            var (operationStatus, response) = await orderService.GetAllOrders();

            return operationStatus switch
            {
                0 => Results.Ok(response),
                1 => Results.Problem(response.ToString(), statusCode: 500),
                _ => Results.BadRequest(InvalidOperationMessage)
            };
        });
        
        // Get order.
        app.MapGet("/order/{id:int}", async (OrderService orderService, int id) =>
        {
            var (operationStatus, response) = await orderService.GetOrder(id);
            
            return operationStatus switch
            {
                0 => Results.Ok(response),
                1 => Results.Problem(response.ToString(), statusCode: 500),
                2 => Results.NotFound(response),
                _ => Results.BadRequest(InvalidOperationMessage)
            };
        });
        
        // Create order.
        app.MapPost("/order/new-order/", async (OrderService orderService, OrderCreateDto newOrder) =>
        {
            var (operationStatus, response) = await orderService.CreateOrder(newOrder);

            return operationStatus switch
            {
                0 => Results.Ok(response.ToString()),
                1 => Results.Problem(response.ToString(), statusCode: 500),
                2 => Results.NotFound(response),
                3 => Results.BadRequest(response),
                _ => Results.BadRequest(InvalidOperationMessage)
            };
        });
    }
}