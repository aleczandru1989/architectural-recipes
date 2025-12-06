using Streamiz.Kafka.Net;
using Streamiz.Kafka.Net.SerDes;
using Streamiz.Kafka.Net.Stream;

namespace Consumer.Services;

class Program
{
    private const string TOPIC_NAME = "app.order.publish";

    static async Task Main(string[] args)
    {
        var kafkaBootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS") ?? "localhost:9092";
        var consumerGroupId = Environment.GetEnvironmentVariable("CONSUMER_GROUP_ID") ?? "app";
        var country = Environment.GetEnvironmentVariable("CONSUMER_GROUP_COUNTRY") ?? "RO";

        var config = new StreamConfig<StringSerDes, StringSerDes>();
        config.ApplicationId = consumerGroupId;
        config.BootstrapServers = kafkaBootstrapServers;

        StreamBuilder builder = new StreamBuilder();

        builder.Stream<string, OrderMessage>(TOPIC_NAME)
            .Filter((k, v, context) => v.Country == country) 
            .To($"app.order.publish.{country}");
        
        Topology topology = builder.Build();
        KafkaStream stream = new KafkaStream(topology, config);

        // Handle clean up on application exit
        Console.CancelKeyPress += (o, e) =>
        {
            Console.WriteLine("\nShutting down gracefully...");
            stream.Dispose();
            Environment.Exit(0);
        };

        Console.WriteLine("Streamiz Kafka.Net application started. Press Ctrl+C to stop.");
        
        try
        {
            await stream.StartAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error starting stream: {ex.Message}");
        }
        finally
        {
            stream.Dispose();
        }
    }
}

public class OrderMessage
{
    public string OrderId { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}