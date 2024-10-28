using EFMBL.ApplicationDbContext;
using EFMBL.IOrderServise;
using EFMBL.OrderRepository;
using EFMBL.OrderService;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


if (!Directory.Exists("Logs"))
{
    Directory.CreateDirectory("Logs");
}

// Настройка Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() 
    .WriteTo.File("Logs/logs.txt", rollingInterval: RollingInterval.Day) 
    .CreateLogger();

builder.Host.UseSerilog(); 

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();


app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke(); 
    }
    catch (Exception ex)
    {
        Log.Error(ex, "An error occurred while processing the request."); 
        context.Response.StatusCode = 500; 
        await context.Response.WriteAsync("An error occurred while processing your request."); 
    }

    if (context.Response.StatusCode == 200)
    {
        Log.Information("Request processed successfully: {Path}", context.Request.Path); 
    }
});


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}


app.MapControllers();
app.Run();
