using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using InzynierkaApiReact.Server.Data;
using InzynierkaApiReact.Server.Services;
using InzynierkaApiReact.Server.Interface;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InzynierkaApiReactServerContext") ?? throw new InvalidOperationException("Connection string 'InzynierkaApiReactServerContext' not found.")));

// Add services to the container.

builder.Services.AddDistributedMemoryCache();

//Dodanie kontrolerów
builder.Services.AddControllers();
//Dodanie swaggera
builder.Services.AddSwaggerGen();
//Dodanie dependency injection dla serwisu
builder.Services.AddScoped<IProductServices, ProductServices>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsAll", builder =>
        builder.AllowAnyOrigin() 
               .AllowAnyMethod()
               .AllowAnyHeader());
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin() // Zezwala na ¿¹dania z dowolnego Ÿród³a
                  .AllowAnyMethod()  // Zezwala na wszystkie metody HTTP (GET, POST, PUT, DELETE)
                  .AllowAnyHeader(); // Zezwala na dowolne nag³ówki w ¿¹daniach
        });
});
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

//Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("CorsAll");

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();


app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
