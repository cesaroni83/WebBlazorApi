using Microsoft.EntityFrameworkCore;
using WebBlazorAPI.Server.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//-------------------- Database --------------------//
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSql")));
builder.Services.AddTransient<SeedDB>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy =>
        {
            policy.WithOrigins("https://localhost:7063")

                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  //.SetIsOriginAllowed(origin => true)
                  .AllowCredentials(); // necesario si usas cookies o JWT
        });
});
var app = builder.Build();
////-------------------- Seed Database --------------------//
SeedData(app);

void SeedData(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var service = scope.ServiceProvider.GetRequiredService<SeedDB>();
    service.SeedAsync().Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazor");
app.UseAuthorization();

app.MapControllers();

app.Run();
