using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XmasApi.Data;
using XmasApi.Models;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<XmasApiContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("XmasApiContext") ?? throw new InvalidOperationException("Connection string 'XmasApiContext' not found.")));

builder.Services.AddDbContext<XmasApiContext>(opt =>
    opt.UseInMemoryDatabase("XmasItems"));

// Add services to the container.

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
