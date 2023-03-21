using AllProjects.DataContext;
using AllProjects.Interface;
using AllProjects.Models;
using Microsoft.EntityFrameworkCore;

namespace AllProjects.Schema
{
    public static class ProductsEndpoints
    {
        public static void MapProductEndpoints(this WebApplication app)
        {
            app.MapGet("/products", List);
            app.MapGet("/products/{id}", Get);
            //app.MapPost("/products", Create).AddFilter<ValidationFilter<Product>>();
            //app.MapPut("/products", Update).AddFilter<ValidationFilter<Product>>();
            app.MapDelete("/products/{id}", Delete);
        }

        public static async Task<IResult> List(SQLDataContext db)
        {
            var result = await db.Products.ToListAsync();
            
            return Results.Ok(result);
        }

        public static async Task<IResult> Get(SQLDataContext db, int id)
        {
            return await db.Products.FindAsync(id) is Product product
                ? Results.Ok(product)
                : Results.NotFound();
        }

        public static async Task<IResult> Create(SQLDataContext db, Product product)
        {
            db.Products.Add(product);
            await db.SaveChangesAsync();

            return Results.Created($"/products/{product.Id}", product);
        }

        public static async Task<IResult> Update(SQLDataContext db, Product updatedProduct)
        {
            var product = await db.Products.FindAsync(updatedProduct.Id);

            if (product is null) return Results.NotFound();

            product.ProductName = updatedProduct.ProductName;
            product.UnitPrice = updatedProduct.UnitPrice;

            await db.SaveChangesAsync();

            return Results.NoContent();
        }

        public static async Task<IResult> Delete(SQLDataContext db, int id)
        {
            if (await db.Products.FindAsync(id) is Product product)
            {
                db.Products.Remove(product);
                await db.SaveChangesAsync();
                return Results.Ok(product);
            }

            return Results.NotFound();
        }
    }
}
