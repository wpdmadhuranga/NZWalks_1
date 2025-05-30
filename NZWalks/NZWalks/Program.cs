using NZWalks.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
//     options =>
// {
//     options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
//     {
//         Version = "v1",
//         Title = "NZ Walks API",
//         Description = "API for managing New Zealand walks"
//     });
// }
    );
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("NZWalksConnectionString");
builder.Services.AddDbContext<NZWalksDbContext1>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
    //     options =>
    // {
    //     options.SwaggerEndpoint("/swagger/v1/swagger.json", "NZ Walks API v1");
    //     options.RoutePrefix = string.Empty; // Optional: makes Swagger UI the home page
    // }
        );

}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

