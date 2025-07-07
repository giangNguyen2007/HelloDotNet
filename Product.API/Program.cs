using MassTransit;
using Product.API.RabbitMqService;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MassTransitConsumer>();
    x.AddConsumer<MassTransitConsumer2>(); // Register the consumer

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost");

        // Configure the receive endpoint and bind the consumer
        cfg.ReceiveEndpoint("order-submitted-queue", e =>
        {
            e.ConfigureConsumer<MassTransitConsumer>(context);
        });
        
        cfg.ReceiveEndpoint("order-submitted-queue2", e =>
        {
            e.ConfigureConsumer<MassTransitConsumer2>(context);
        });
    });
});

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

