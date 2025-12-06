
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Confluent.Kafka;
using System.Text.Json;

namespace Consumer.Services;

public class OrderConsumerService : BackgroundService
{
    private readonly ILogger<OrderConsumerService> _logger;
    private readonly IConsumer<string, string> _consumer;
    private readonly string _consumerInstanceId;
    
    private string TOPIC_NAME = "app.order.publish"

    public OrderConsumerService(ILogger<OrderConsumerService> logger)
    {
        _logger = logger;
        var instanceName = Environment.GetEnvironmentVariable("CONSUMER_INSTANCE_NAME") ?? Environment.MachineName;
        _consumerInstanceId = instanceName + "-" + Guid.NewGuid().ToString("N")[..8];

        var kafkaBootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS") ?? "localhost:9092";
        var consumerGroupId = Environment.GetEnvironmentVariable("CONSUMER_GROUP_ID") ?? "app";

        var config = new ConsumerConfig
        {
            BootstrapServers = kafkaBootstrapServers,
            GroupId = consumerGroupId,
            ClientId = _consumerInstanceId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false,
            SessionTimeoutMs = 6000,
            HeartbeatIntervalMs = 2000,
            MaxPollIntervalMs = 300000,
            EnablePartitionEof = false
        };

        _consumer = new ConsumerBuilder<string, string>(config)
            .SetErrorHandler((_, e) => _logger.LogError($"Consumer error: {e.Reason}"))
            .SetPartitionsAssignedHandler((c, partitions) =>
            {
                _logger.LogInformation($"[CONSUMER {_consumerInstanceId}] Assigned partitions: [{string.Join(", ", partitions)}]");
            })
            .SetPartitionsRevokedHandler((c, partitions) =>
            {
                _logger.LogInformation($"[CONSUMER {_consumerInstanceId}] Revoked partitions: [{string.Join(", ", partitions)}]");
            })
            .Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(TOPIC_NAME);
        _logger.LogInformation($"[CONSUMER {_consumerInstanceId}] Started consuming messages from topic: {TOPIC_NAME}");

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(stoppingToken);

                    if (consumeResult?.Message != null)
                    {
                        _logger.LogInformation($"[CONSUMER {_consumerInstanceId}] Received message: Key={consumeResult.Message.Key}, Value={consumeResult.Message.Value}");
                        
                        // Process the message here
                        await ProcessMessage(consumeResult.Message);
                        
                        // Commit the offset
                        _consumer.Commit(consumeResult);
                    }
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError($"[CONSUMER {_consumerInstanceId}] Error consuming message: {ex.Error.Reason}");
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError($"[CONSUMER {_consumerInstanceId}] Unexpected error: {ex.Message}");
                }
            }
        }
        finally
        {
            _consumer.Close();
            _consumer.Dispose();
            _logger.LogInformation($"[CONSUMER {_consumerInstanceId}] Consumer stopped and disposed");
        }
    }

    private async Task ProcessMessage(Message<string, string> message)
    {
        try
        {
            // Deserialize and process the order message
            var orderMessage = JsonSerializer.Deserialize<OrderMessage>(message.Value);
            _logger.LogInformation($"[CONSUMER {_consumerInstanceId}] Processing order: {orderMessage?.OrderId} - {orderMessage?.Product}");
            
            // Simulate processing time
            await Task.Delay(Random.Shared.Next(1000, 3000));
            
            _logger.LogInformation($"[CONSUMER {_consumerInstanceId}] Successfully processed order: {orderMessage?.OrderId}");
        }
        catch (JsonException ex)
        {
            _logger.LogError($"[CONSUMER {_consumerInstanceId}] Error deserializing message: {ex.Message}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"[CONSUMER {_consumerInstanceId}] Error processing message: {ex.Message}");
        }
    }

    public override void Dispose()
    {
        _consumer?.Dispose();
        base.Dispose();
    }
}

public class OrderMessage
{
    public string OrderId { get; set; } = string.Empty;
    public string Product { get; set; } = string.Empty;
}