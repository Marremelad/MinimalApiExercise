using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MinimalApiExercise.Data;
using MinimalApiExercise.DTOs;
using MinimalApiExercise.DTOs.CustomerDTOs;
using MinimalApiExercise.DTOs.OrderDTOs;
using MinimalApiExercise.Models;

namespace MinimalApiExercise.Services;

public class CustomerService(StoreDbContext context)
{
    private const string OperationsSuccessfulMessage = "Operation successful";
    private const string NotFoundMessage = "not found";
    private const string OrderTerminatedMessage = "Order terminated:";
    
    // Get all customers.
    public async Task<(int, object)> GetAllCustomers()
    {
        List<CustomerDto> customers;
        try
        {
            customers = await context.Customers
                .Select(c => new CustomerDto
                {
                    CustomerId = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Phone = c.Phone

                }).ToListAsync();
        }
        catch (Exception e)
        {
            return (1, e);
        }

        return (0, customers);
    }

    // Get customer by ID.
    public async Task<(int, object)> GetCustomer(int id)
    {
        CustomerDto? customer;
        try
        {
            customer = await context.Customers
                .Where(c => c.Id == id)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Email = c.Email,
                    Phone = c.Phone
                })
                .FirstOrDefaultAsync();

            if (customer == null) return (2, "Customer " + NotFoundMessage);
        }
        catch (Exception e)
        {
            return (1, e);
        }

        return (0, customer);
    }
    
    // Get customer orders by ID.
    public async Task<(int, object)> GetCustomerOrders(int id)
    {
        object? customerOrders;
        try
        {
            customerOrders = await context.Customers
                .Where(c => c.Id == id)
                .Select(c => new
                {
                    CustomerName = $"{c.FirstName} {c.LastName}",
                    Orders = c.Orders!.Select(o => new OrderDto
                    {
                        OrderId = o.Id,
                        CustomerId = o.CustomerIdFk,
                        OrderedProducts = o.OrderProducts!
                            .Select(op => new ProductDto
                            {
                                ProductId = op.Product!.Id,
                                ProductName = op.Product.Name,
                                ProductDescription = op.Product.Description,
                                ProductPrice = op.Product.Price
                            })
                    })
                })
                .FirstOrDefaultAsync();

            if (customerOrders == null) return (2, "Customer " + NotFoundMessage);
        }
        catch (Exception e)
        {
            return (1, e);
        }

        return (0, customerOrders);
    }
    
    // Create customer.
    public async Task<(int, object)> CreateCustomer(CustomerCreateDto newCustomer)
    {
        try
        {
            var validationContext = new ValidationContext(newCustomer);
            var validationResult = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(newCustomer, validationContext, validationResult, true);

            if (!isValid) return (3, validationResult.Select(v => v.ErrorMessage));
        
            var customer = new Customer
            {
                FirstName = newCustomer.FirstName,
                LastName = newCustomer.LastName,
                Email = newCustomer.Email,
                Phone = newCustomer.Phone
            };

            context.Customers.Add(customer);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return (1, e);
        }

        return (0, OperationsSuccessfulMessage);
    }

    // Delete customer.
    public async Task<(int, object)> DeleteCustomer(int id)
    {
        try
        {
            var customer = await context.Customers
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null) return (2, "Customer " + NotFoundMessage);

            context.Customers.Remove(customer);
            await context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return (1, e);
        }
        
        return (0, OperationsSuccessfulMessage);
    }
}