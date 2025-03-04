using MinimalApiExercise.Services;

namespace MinimalApiExercise.Endpoints;

public class OrderEndpoints
{
    public static void RegisterEndpoints(WebApplication app)
    {
        // Get all orders.
        app.MapGet("/orders", async (OrderService orderService) => await orderService.GetAllOrders());
        
        // Get order.
        app.MapGet("/order/{id:int}", async (OrderService orderService, int id) => await orderService.GetOrder(id));
    }
}