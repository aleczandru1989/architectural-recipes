using Steeltoe.Discovery.Eureka;
using Steeltoe.Discovery.Configuration;
using Steeltoe.Discovery.HttpClients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEurekaDiscoveryClient();
builder.Services.AddHttpClient("product-orders", client =>
    {
        // Use the AppName registered in Eureka as the Host
        client.BaseAddress = new Uri("http://product-orders/"); 
    })
    .AddServiceDiscovery();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();