using Business.Services;
using Data.Context;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", x => //TODO: Replace with more restrictive policy in production
    {
        x.WithOrigins("http://localhost:5173", "https://localhost:5001", "https://localhost:7252", "http://localhost:5240") // adjust ports if needed
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionProfiles")));
builder.Services.AddScoped<DataContext>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<UserProfileService, UserProfileService>();
var app = builder.Build();


app.MapOpenApi();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventService API V1");
    c.RoutePrefix = string.Empty;
});


app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization(); //[Authorize] not implemented, TODO: Change authservice to use JWT tokens and implement [Authorize] attribute in controllers

app.MapControllers();

app.Run();
