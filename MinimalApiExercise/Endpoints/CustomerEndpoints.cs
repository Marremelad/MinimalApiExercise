using MinimalApiExercise.DTOs.CustomerDTOs;
using MinimalApiExercise.Services;

namespace MinimalApiExercise.Endpoints;

public abstract class CustomerEndpoints
{
    public static void RegisterEndpoints(WebApplication app)
    {
        // Get all customers.
        app.MapGet("/customers", async (CustomerService customerService) => 
            await customerService.GetAllCustomers());

        // Get customer by ID.
        app.MapGet("/customers/{id:int}", async (CustomerService customerService, int id) => 
            await customerService.GetCustomer(id));
        
        // Get customer orders by ID.
        app.MapGet("/customers/{id:int}/orders", async (CustomerService customerService, int id) => 
            await customerService.GetCustomerOrders(id));
        
        // Create customer.
        app.MapPost("/customers/create", async (CustomerService customerService, CustomerCreateDto newCustomer) => 
            await customerService.CreateCustomer(newCustomer));

        // Delete customer.
        app.MapDelete("/customers/delete/{id:int}", async (CustomerService customerService, int id) => 
            await customerService.DeleteCustomer(id));
    }
}