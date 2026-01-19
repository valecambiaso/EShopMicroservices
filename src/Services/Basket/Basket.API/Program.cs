using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCarter(); //Injects carter related classes

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly); //Registers all MediatR related classes from the assembly
    config.AddOpenBehavior(typeof(ValidationBehavior<,>)); //Adds validation behavior to MediatR pipeline
    config.AddOpenBehavior(typeof(LoggingBehavior<,>)); //Adds logging behavior to MediatR pipeline
});

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!); //Configures Marten with the database connection string
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName); //Configures the ShoppingCart document with UserName as the identity
}).UseLightweightSessions(); // To get better performance with Marten

builder.Services.AddScoped<IBasketRepository, BasketRepository>(); //Registers the BasketRepository for dependency injection
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>(); //Decorates the BasketRepository with caching functionality

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis"); //Configures Redis cache with the connection string
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>(); //Registers custom exception handler

builder.Services.AddHealthChecks() 
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter(); //Injects carter related classes

app.UseExceptionHandler(options => { }); //The actual exception handling is done by the CustomExceptionHandler registered earlier, so we indicate that with the empty options

app.UseHealthChecks("/health",  // Health endpoint
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse //Custom healthcheck response
    });

app.Run();
