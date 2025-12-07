using Filter.Abstractions.Messages;
using Streamiz.Kafka.Net;
using Streamiz.Kafka.Net.Crosscutting;
using Streamiz.Kafka.Net.SerDes;
using Streamiz.Kafka.Net.State;
using Streamiz.Kafka.Net.Stream;
using Streamiz.Kafka.Net.Table;

namespace EventFilter;

class Program
{
    static async Task Main(string[] args)
    {
        var kafkaBootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS") ?? "localhost:9092";
        var consumerGroupId = Environment.GetEnvironmentVariable("CONSUMER_GROUP_ID") ?? "app";
     
        // Configure for string keys and JSON values
        var config = new StreamConfig<StringSerDes, JsonSerDes<OrderMessage>>();
        config.ApplicationId = consumerGroupId;
        config.BootstrapServers = kafkaBootstrapServers;
        config.CommitIntervalMs = 1000;

        StreamBuilder builder = new StreamBuilder();

        var stringSerDes = new StringSerDes();
        var orderMessageSerDes = new JsonSerDes<OrderMessage>();
        var orderMessageListSerDes = new OrderMessageListSerDes();
        
        
        builder.Stream<string, OrderMessage>("app.order.publish", stringSerDes, orderMessageSerDes)
            .GroupByKey()
            .WindowedBy(  TumblingWindowOptions.Of(TimeSpan.FromSeconds(15)))
            .Aggregate<List<OrderMessage>>(
                () => new List<OrderMessage>(),
                (key, value, aggregate) => 
                {
                    aggregate.Add(value);
                    return aggregate;
                }, RocksDbWindows

                    .As<string, List<OrderMessage>>("order-aggregation-store")
                    .WithKeySerdes(stringSerDes)
                    .WithValueSerdes(orderMessageListSerDes))
            .ToStream()
            .Peek((windowedKey, values, c) => 
            {
                Console.WriteLine($"Batch size: {values.Count} messages for key '{windowedKey.Key}' in window {windowedKey.Window.StartTime} - {windowedKey.Window.EndTime}");
            })
            .To("app.order.publish.RO"); // Use Process, not Transform
        
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
