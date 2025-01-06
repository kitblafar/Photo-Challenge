using System.Diagnostics;
using System.Security.Authentication.ExtendedProtection;
using PhotoChallengeAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PhotoChallengeAPIContext>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://kittyfarren.dev");
                          policy.WithMethods("GET", "POST", "PATCH", "DELETE", "OPTIONS");
                          policy.AllowAnyHeader();
                      });
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

#if DEBUG
app.UseSwagger();
app.UseSwaggerUI();
#endif

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
//app.UseExceptionHandler("/error");

app.UseAuthorization();

app.MapControllers();

app.Run();
