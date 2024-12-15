using Microsoft.AspNetCore.Mvc;
namespace Chapter5Routes.Controllers;

public class ProductsController : Controller
{
    private static readonly List<Product> Products = Enumerable.Range(1, 100)
        .Select(i => new Product
        {
            Id = i,
            Category = $"Category{i % 10 + 1}",
            Slug = $"Product-{i}",
            ExpirationDate = new DateTime(2024, 12, i % 31 + 1) // Date dinamiche
        })
        .ToList();

    [HttpGet]
    public IActionResult Expiration(int year, int month, int day, string category, string slug)
    {
        var product = Products.FirstOrDefault(p =>
            p.ExpirationDate.Year == year &&
            p.ExpirationDate.Month == month &&
            p.ExpirationDate.Day == day &&
            p.Category == category &&
            p.Slug == slug);

        if (product == null)
        {
            return NotFound(new { Message = "Product not found." });
        }

        return Json(new
        {
            product.Id,
            product.Category,
            product.Slug,
            ExpirationDate = product.ExpirationDate.ToString("yyyy-MM-dd"),
            Message = $"Product '{product.Slug}' in category '{product.Category}' expires on {product.ExpirationDate:yyyy-MM-dd}."
        });
    }

    [HttpGet]
    [Route("products")] // Endpoint per tutti i prodotti
    public IActionResult GetAllProducts()
    {
        return Json(Products);
    }
}

// Modello di prodotto
public class Product
{
    public int Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; }
}
