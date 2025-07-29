using MassTransit;
using Order.API.GRPC;
using Order.API.RabbitMqService;
using Product;

var builder = WebApplication.CreateBuilder(args);


// builder.Services.AddMassTransit(x =>
// {
//     x.AddConsumer<MassTransitConsumer>();
//     x.AddConsumer<MassTransitConsumer2>(); // Register the consumer
//
//     x.UsingRabbitMq((context, cfg) =>
//     {
//         cfg.Host("localhost");
//
//         // Configure the receive endpoint and bind the consumer
//         cfg.ReceiveEndpoint("order-submitted-queue", e =>
//         {
//             e.ConfigureConsumer<MassTransitConsumer>(context);
//         });
//         
//         cfg.ReceiveEndpoint("order-submitted-queue2", e =>
//         {
//             e.ConfigureConsumer<MassTransitConsumer2>(context);
//         });
//     });
// });

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddGrpcClient<ProductService.ProductServiceClient>(o =>
{
    o.Address = new Uri("https://localhost:7155"); // or your service DNS in Kubernetes
});

builder.Services.AddScoped<GrpcClientService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

namespace Order.API
{
    public partial class OrderApiProgram {}
}