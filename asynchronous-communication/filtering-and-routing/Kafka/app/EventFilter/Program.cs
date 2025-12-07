using Filter.Abstractions.Messages;
using Streamiz.Kafka.Net;
using Streamiz.Kafka.Net.SerDes;
using Streamiz.Kafka.Net.Stream;

namespace EventFilter;

class Program
{
    private const string TOPIC_NAME = "app.order.publish";
    private static string[] VALID_COUNTRIES = new[] { "RO", "EU", "US" };
    
    static async Task Main(string[] args)
    {
        var kafkaBootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS") ?? "localhost:9092";
        var consumerGroupId = Environment.GetEnvironmentVariable("CONSUMER_GROUP_ID") ?? "app";
     
        // Configure for string keys and JSON values
        var config = new StreamConfig<StringSerDes, JsonSerDes<OrderMessage>>();
        config.ApplicationId = consumerGroupId;
        config.BootstrapServers = kafkaBootstrapServers;

        StreamBuilder builder = new StreamBuilder();

        builder.Stream<string, OrderMessage>(TOPIC_NAME)
            .Filter((k, v, context) =>
            {
                
                Console.WriteLine($"[FILTER] Processing message - Key: {k}, OrderId: {v?.OrderId}, Country: {v?.Country}, Product: {v?.Product}");
                
                bool shouldPass = v?.Country != null && VALID_COUNTRIES.Contains(v.Country);

                Console.WriteLine(shouldPass
                    ? $"[FILTER] ✅ PASSED - Message matches country '{v?.Country}' - OrderId: {v.OrderId}"
                    : $"[FILTER] ❌ FILTERED OUT - Message country '{v?.Country}' does not match any of the allowed countries: '{string.Join(", ", VALID_COUNTRIES) }' - OrderId: {v?.OrderId}");

                return shouldPass;
            }) 
            .Peek((k, v, context) =>
            {
                string targetTopic = $"app.order.publish.{v.Country.ToLower()}";
                Console.WriteLine($"[ROUTING] 🚀 Routing to '{targetTopic}' - OrderId: {v?.OrderId}");
            })
            .To((k, v, context) => $"app.order.publish.{v.Country}");
        
        Topology topology = builder.Build();
        KafkaStream stream = new KafkaStream(topology, config);

        // Handle clean up on application exit
        Console.CancelKeyPress += (o, e) =>
        {
            Console.WriteLine("\nShutting down gracefully...");
            stream.Dispose();
            Environment.Exit(0);
        };
        
        try
        {
            await stream.StartAsync();
            
            // Keep the application running until cancelled
            await Task.Delay(-1);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error starting stream: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }
        finally
        {
            stream.Dispose();
        }
    }
}