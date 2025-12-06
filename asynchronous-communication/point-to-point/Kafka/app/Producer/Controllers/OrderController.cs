using System.Text.Json;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Producer.Config;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IProducer<string, string> _producer;
    private readonly ILogger<OrderController> _logger;
    
    public OrderController(IProducer<string, string> producer, ILogger<OrderController> logger)
    {
        _producer = producer;
        _logger = logger;
    }

    [HttpPost("Send")]
    public async Task<IActionResult> SendOrder([FromBody] OrderMessage order)
    {
        try
        {
            Message<string, string> message = new Message<string, string>
            {
                Key = order.OrderId,
                Value = JsonSerializer.Serialize(order),
            };

            DeliveryResult<string, string>? deliveryReport = await _producer.ProduceAsync(KafkaConfig.TopicName, message);
                
            _logger.LogInformation($"Message sent to topic {KafkaConfig.TopicName} at offset {deliveryReport.Offset}");
                
            return Ok(new { 
                Status = "Success", 
                Topic = KafkaConfig.TopicName, 
                Offset = deliveryReport.Offset.Value,
                OrderId = order.OrderId
            });
        }
        catch (ProduceException<string, string> ex)
        {
            _logger.LogError($"Error producing message: {ex.Error.Reason}");
            return StatusCode(500, new { Error = ex.Error.Reason });
        }
    }
}

public class OrderMessage
{
    public string OrderId { get; set; } = string.Empty;
    public string Product { get; set; } = string.Empty;
}