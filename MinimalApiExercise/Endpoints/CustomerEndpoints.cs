using MinimalApiExercise.DTOs.CustomerDTOs;
using MinimalApiExercise.Services;

namespace MinimalApiExercise.Endpoints;

public abstract class CustomerEndpoints
{
    private const string InvalidOperationMessage = "Invalid operation status";
    
    public static void RegisterEndpoints(WebApplication app)
    {
        // Get all customers.
        app.MapGet("/customers", async (CustomerService customerService) =>
        {
            var (operationStatus, response) = await customerService.GetAllCustomers();

            return operationStatus switch
            {
                0 => Results.Ok(response),
                1 => Results.Problem(response.ToString(), statusCode: 500),
                _ => Results.BadRequest(InvalidOperationMessage)
            };
        });

        // Get customer by ID.
        app.MapGet("/customers/{id:int}", async (CustomerService customerService, int id) =>
        {
            var (operationStatus, response) = await customerService.GetCustomer(id);

            return operationStatus switch
            {
                0 => Results.Ok(response),
                1 => Results.Problem(response.ToString(), statusCode: 500),
                2 => Results.NotFound(response),
                _ => Results.BadRequest(InvalidOperationMessage)
            };
        });
        
        // Get customer orders by ID.
        app.MapGet("/customers/{id:int}/orders", async (CustomerService customerService, int id) =>
        {
            var (operationStatus, response) = await customerService.GetCustomerOrders(id);
            
            return operationStatus switch
            {
                0 => Results.Ok(response),
                1 => Results.Problem(response.ToString(), statusCode: 500),
                2 => Results.NotFound(response),
                _ => Results.BadRequest(InvalidOperationMessage)
            };
            
        });
        
        // Create customer.
        app.MapPost("/customers/create", async (CustomerService customerService, CustomerCreateDto newCustomer) =>
        {
            var (operationStatus, response) = await customerService.CreateCustomer(newCustomer);
            
            return operationStatus switch
            {
                0 => Results.Ok(response),
                1 => Results.Problem(response.ToString(), statusCode: 500),
                _ => Results.BadRequest(InvalidOperationMessage)
            };
        });

        // Delete customer.
        app.MapDelete("/customers/delete/{id:int}", async (CustomerService customerService, int id) =>
        {
            var (operationStatus, response) = await customerService.DeleteCustomer(id);

            return operationStatus switch
            {
                0 => Results.Ok(response),
                1 => Results.Problem(response.ToString(), statusCode: 500),
                2 => Results.NotFound(response),
                _ => Results.BadRequest(InvalidOperationMessage)
            };
        });
    }
}