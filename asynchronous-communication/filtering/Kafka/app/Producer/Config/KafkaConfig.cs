using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace Producer.Config;

public static class KafkaConfig
{
    public const string TopicName = "app.order.publish";

    public static async Task Configure()
    {
        var kafkaBootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS") ?? "localhost:9092";
        Console.WriteLine(kafkaBootstrapServers);
        AdminClientConfig adminConfig = new()
        {
            BootstrapServers = kafkaBootstrapServers
        };

        using var adminClient = new AdminClientBuilder(adminConfig).Build();

        TopicSpecification[] topicsToCreate =
        [
            new TopicSpecification { Name = TopicName, NumPartitions = 6, ReplicationFactor = 1 }
        ];

        try
        {
            await adminClient.CreateTopicsAsync(topicsToCreate);
            Console.WriteLine($"✅ Topic '{TopicName}' created successfully");
        }
        catch (CreateTopicsException ex)
        {
            foreach (CreateTopicReport? result in ex.Results)
            {
                Console.WriteLine(result.Error.Code == ErrorCode.TopicAlreadyExists
                    ? $"ℹ️ Topic '{result.Topic}' already exists"
                    : $"❌ Failed to create topic '{result.Topic}': {result.Error.Reason}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error creating topics: {ex.Message}");
            throw;
        }
    }
}