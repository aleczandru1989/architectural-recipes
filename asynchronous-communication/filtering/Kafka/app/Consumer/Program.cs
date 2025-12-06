using Consumer.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<OrderConsumerService>();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);

var host = builder.Build();

var consumerInstanceName = Environment.GetEnvironmentVariable("CONSUMER_INSTANCE_NAME") ?? Environment.MachineName;
Console.WriteLine($"🚀 Starting Kafka Consumer Instance: {consumerInstanceName}");

host.Run();