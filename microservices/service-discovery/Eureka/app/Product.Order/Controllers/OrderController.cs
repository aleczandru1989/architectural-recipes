using Microsoft.AspNetCore.Mvc;

namespace Product.Order.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{

    [HttpGet(Name = "Orders")]
    public IEnumerable<Order> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Order
            {
                CreateDate = DateTime.Now,
                ProductId = Guid.NewGuid()
            })
            .ToArray();
    }
}