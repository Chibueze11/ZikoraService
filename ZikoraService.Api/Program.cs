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

await MiddlewareExtensions.ApplyMigrationsAsync(app.Services);


app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCustomMiddlewares();
app.MapControllers();
app.Run();
