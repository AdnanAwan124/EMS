using EMS.Infrastructure;
using EMS.Application;

var builder = WebApplication.CreateBuilder(args);
var allowedOriginsPolicy = "allowedOrigins";

// Add services to the container.

var allowedOrigins = builder.Configuration["AllowedOrigins"].ToString();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowedOriginsPolicy,
        policy =>
        {
            policy.WithOrigins(allowedOrigins.Split(","))
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Infra services
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowedOriginsPolicy);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
