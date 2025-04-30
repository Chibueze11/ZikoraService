using Microsoft.EntityFrameworkCore;
using ZikoraService.Api.Extensions;
using ZikoraService.Application.Extensions;
using ZikoraService.Application.Mappings;
using ZikoraService.Infrastructure.Extensions;
using ZikoraService.Infrastructure.Persistence.DbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbContext = services.GetRequiredService<AppDbContext>(); 
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database");
    }
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCustomMiddlewares();
app.MapControllers();
app.Run();
