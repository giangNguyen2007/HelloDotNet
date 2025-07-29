using System.Reflection.Metadata.Ecma335;
using System.Text;
using Game.API.AsyncService;
using Game.API.Data;
using Game.API.Interfaces;
using Game.API.Repository;
using Game.API.Data;
using Game.API.Dtos;
using Game.API.GRPC.Service;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SharedLibrary.MassTransit.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    
    x.AddConsumer<UpdateStockConsumerService>();
    
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
             h.Username("guest"); 
             h.Password("guest");
        });
        
        cfg.ReceiveEndpoint("update-stock-queue", e =>
            e.ConfigureConsumer<UpdateStockConsumerService>(context)
        );
       
    });
    
});

builder.Services.AddControllers();

// builder.Services.AddControllers().AddNewtonsoftJson(options =>
// {
//     options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
// });

builder.Services.AddDbContext<GameDBContext>(option =>
{
    option.UseSqlite(builder.Configuration.GetConnectionString("GameStore"));
});

// builder.Services.AddIdentity<AppUserModel, IdentityRole>(options =>
// {
//     options.Password.RequireDigit = true;
//     options.Password.RequiredLength = 6;
// });

var key = Encoding.ASCII.GetBytes("096c0a72c31f9a2d65126d8e8a401a2ab2f2e21d0a282a6ffe6642bbef65ffd9");
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("OnlyAdmin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("admin"); 
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();



var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<ProductGrpcService>(); // New gRPC service

if (app.Environment.IsDevelopment())
{
    app.MapGrpcReflectionService();
}


app.Run();

namespace Game.API
{
    public partial class GameApiProgram {}
}