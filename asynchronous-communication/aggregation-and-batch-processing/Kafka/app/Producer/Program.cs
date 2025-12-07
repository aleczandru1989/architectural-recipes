using Confluent.Kafka;
using Producer.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add health checks
builder.Services.AddHealthChecks();

// Get Kafka bootstrap servers from environment variable or use default
var kafkaBootstrapServers = Environment.GetEnvironmentVariable("KAFKA_BOOTSTRAP_SERVERS") ?? "localhost:9092";
Console.WriteLine($"🔧 Using Kafka Bootstrap Servers: {kafkaBootstrapServers}");

builder.Services.AddSingleton<IProducer<string, string>>(sp =>
{
    var cfg = new ProducerConfig
    {
        BootstrapServers = kafkaBootstrapServers,
        ClientId = "kafka-producer",
        Acks = Acks.All,
        RetryBackoffMs = 1000,
        MessageSendMaxRetries = 3,
        RequestTimeoutMs = 30000
    };

    Console.WriteLine($"🔧 Producer Config - BootstrapServers: {cfg.BootstrapServers}");
    return new ProducerBuilder<string, string>(cfg).Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

// Map health check endpoint
app.MapHealthChecks("/health");

// Wait for Kafka and configure topics with retry logic
await WaitForKafkaAndConfigureTopics();

// Ensure producer is disposed properly
app.Lifetime.ApplicationStopping.Register(() =>
{
    var producer = app.Services.GetService<IProducer<string, string>>();
    producer?.Dispose();
});

app.Run();

async Task WaitForKafkaAndConfigureTopics()
{
    const int maxRetries = 10;
    const int delayMs = 5000;
    
    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            Console.WriteLine($"🔄 Attempting to configure Kafka topics (attempt {i + 1}/{maxRetries})...");
            await KafkaConfig.Configure();
            Console.WriteLine("✅ Kafka topics configured successfully!");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Failed to configure Kafka topics: {ex.Message}");
            
            if (i < maxRetries - 1)
            {
                Console.WriteLine($"⏳ Retrying in {delayMs}ms...");
                await Task.Delay(delayMs);
            }
            else
            {
                Console.WriteLine("❌ Max retries exceeded. Starting application without topic configuration.");
            }
        }
    }
}