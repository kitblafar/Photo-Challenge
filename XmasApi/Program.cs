using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XmasApi.Data;
using XmasApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<XmasApiContext>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://kittyfarren.dev",
                                              "http://10.75.12.141:5173/",
                                              "http://10.75.12.141:5173/",
                                              "http://10.75.12.141:3000/api/xmasapi");
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
