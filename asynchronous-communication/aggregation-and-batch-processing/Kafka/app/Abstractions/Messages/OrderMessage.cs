namespace Filter.Abstractions.Messages;

public class OrderMessage
{
    public string OrderId { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Product { get; set; } = string.Empty;
}