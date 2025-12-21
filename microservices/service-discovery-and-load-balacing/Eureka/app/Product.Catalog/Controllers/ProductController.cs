using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Product.Catalog.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IHttpClientFactory factory) : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    private HttpClient _httpClient => factory.CreateClient("product-orders");



    [HttpGet]
    public IEnumerable<Product> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Product
            {
                Date = DateTime.Now,
                Price = Random.Shared.Next(-20, 55),
                Name = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet("order")]
    public async Task<IActionResult> Order()
    {
        Console.WriteLine("Step 1: Starting order method in ProductController");
        
        try
        {
            Console.WriteLine("Step 2: Using Steeltoe Connector for service discovery and load balancing");
            Console.WriteLine("Step 3: Calling product-orders service with automatic load balancing");
            
            // Steeltoe Connector handles service discovery and load balancing automatically
            var orderServiceUrl = "Order";
            Console.WriteLine($"Step 4: Calling service URL: {orderServiceUrl}");

            var response = await _httpClient.GetAsync(orderServiceUrl);
            
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Step 5: Successfully received response from OrderController");
                var jsonContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Step 6: Response content: {jsonContent}");
                
                var orders = JsonSerializer.Deserialize<Order[]>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                Console.WriteLine($"Step 7: Successfully deserialized {orders?.Length ?? 0} orders");
                Console.WriteLine("Step 8: Order method completed successfully");
                
                return Ok(orders);
            }
            else
            {
                Console.WriteLine($"Step 5: ERROR - Failed to call OrderController. Status: {response.StatusCode}");
                return StatusCode((int)response.StatusCode, "Failed to retrieve orders");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Step ERROR: Exception occurred in order method: {ex.Message}");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}