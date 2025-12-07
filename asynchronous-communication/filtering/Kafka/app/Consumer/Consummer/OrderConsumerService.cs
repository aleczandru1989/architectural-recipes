using System.Text.Json;
using Confluent.Kafka;
using Filter.Abstractions.Messages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Consumer.Consummer;

public class OrderConsumerService : BackgroundService
{
    private readonly ILogger<OrderConsumerService> _logger;
    private IConsumer<string, string> _consumer;
    private readonly string _consumerInstanceId;
    private readonly string _topicName;
    
    public OrderConsumerService(ILogger<OrderConsumerService> logger)
    {
        _logger = logger;
        _consumerInstanceId = CreateConsummerInstanceId();
        _topicName = CreateTopicName();
        _consumer = ConfigureConsumer();
    }

    private string CreateConsummerInstanceId()
    {
        string instanceName = Environment.GetEnvironmentVariable("CONSUMER_INSTANCE_NAME") ?? Environment.MachineName;
        instanceName+= "-" + Guid.NewGuid().ToString("N")[..8];
        
        _logger.LogInformation($"STARTED: CONSUMER with consumerInstanceId: {instanceName}");
        
        return instanceName;
    }

    private string CreateTopicName()
    {
        string country = Environment.GetEnvironmentVariable("CONSUMER_GROUP_COUNTRY") ?? "RO";
        
        string topicName =  $"app.order.publish.{country}";

        _logger.LogInformation($"STARTED: CONSUMER with topic subscription in: {topicName}");

        return topicName;
    }

    private IConsumer<string, string> ConfigureConsumer()
    {
        string kafkaBootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS") ?? "localhost:9092";
        _logger.LogInformation($"STARTED: CONSUMER wwith kafka bootstrap server: {kafkaBootstrapServers}");

        string consumerGroupId = Environment.GetEnvironmentVariable("CONSUMER_GROUP_ID") ?? "app";
        _logger.LogInformation($"STARTED: CONSUMER with consumer group id: {consumerGroupId}");

        ConsumerConfig config = new ConsumerConfig
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

        return new ConsumerBuilder<string, string>(config)
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
        _consumer.Subscribe(_topicName);
        
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
            
            _logger.LogInformation($"[CONSUMER {_consumerInstanceId}] Successfully processed order: {orderMessage?.OrderId} with product: {orderMessage?.Product} in country {orderMessage.Country}");
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