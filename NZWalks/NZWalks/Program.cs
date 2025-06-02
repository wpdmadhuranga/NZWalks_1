using NZWalks.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "NZWalks API", Version = "v1" });
});
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("NZWalksConnectionString");
builder.Services.AddDbContext<NZWalksDbContext1>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NZWalks API v1");
    });

}

app.UseHttpsRedirection();
app.UseAuthorization();

// Add error handling middleware here
app.UseExceptionHandler("/error"); // Handles uncaught exceptions
app.UseStatusCodePagesWithReExecute("/error/{0}"); // Handles HTTP status codes (404, 500, etc.)

app.MapControllers();
app.Run();

