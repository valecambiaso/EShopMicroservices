using Discount.Grpc.Data;
using Discount.Grpc.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddDbContext<DiscountContext>(opts =>  // Registers the DiscountContext with the dependency injection container
    opts.UseSqlite(builder.Configuration.GetConnectionString("Database"))); // Configures the DbContext to use SQLite with the specified connection string

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMigration(); // Applies any pending migrations to the database
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
