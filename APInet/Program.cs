using APInet.Data;
using APInet.Models;

namespace APInet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            MongoCRUD db = new MongoCRUD("Shop");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();


            //CRUD OPERATIONS
            //POST
            app.MapPost("/product", async (Products product) =>
            {
                var ShopDB = await db.AddProduct("Products", product);
                return Results.Ok(ShopDB);
            });

            //GET
            app.MapGet("/products", async () =>
            {
                var products = await db.GetAllProducts("Products");
                return Results.Ok(products);
            });

            //GET BY ID
            app.MapGet("/product{id}", async (string id) =>
            {
                var product = await db.GetProductsById(id);
                if (product == null)
                    return Results.NotFound($"Product with ID {id} not found");
                return Results.Ok(product);
            });


            //UPDATE
            app.MapPut("products/{id}", async (Products productInput, string id) =>
            {
                var updatedProduct = await db.UpdatedProduct("Products", productInput);
                return Results.Ok(updatedProduct);
            });

            //DELETE
            app.MapDelete("/product/{id}", async (string id) =>
            {
                var product = await db.DeleteProduct("Products", id);
                return Results.Ok(product);
            });
            app.Run();
        }
    }
}
