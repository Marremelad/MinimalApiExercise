using Microsoft.EntityFrameworkCore;
using MinimalApiExercise.Data;
using MinimalApiExercise.Endpoints;
using MinimalApiExercise.Services;


namespace MinimalApiExercise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<OrderService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            CustomerEndpoints.RegisterEndpoints(app);
            ProductEndpoints.RegisterEndpoints(app);
            OrderEndpoints.RegisterEndpoints(app);
            
            app.Run();
        }
    }
}
