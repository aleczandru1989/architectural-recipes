namespace Product.Catalog.Controllers;

public class Order
{
    public DateTime CreateDate { get; set; }
    
    public Guid ProductId { get; set; }
}