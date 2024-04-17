using ConciertosSoloApi.Data;
using ConciertosSoloApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString =
    builder.Configuration.GetConnectionString("SQL");
builder.Services.AddTransient<RepositoryArtistas>();
builder.Services.AddTransient<RepositoryConciertos>();
builder.Services.AddTransient<RepositoryGeneros>();
builder.Services.AddTransient<RepositoryPeticiones>();
builder.Services.AddTransient<RepositoryProvincias>();
builder.Services.AddTransient<RepositoryPublicaciones>();
builder.Services.AddTransient<RepositoryRelaciones>();
builder.Services.AddTransient<RepositorySalas>();
builder.Services.AddTransient<RepositorySesion>();

builder.Services.AddDbContext<ConciertosSoloContext>
    (options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Api Conciertos Solo :D",
        Description = "Api en proceso",
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(url: "/swagger/v1/swagger.json"
        , name: "Api Conciertos Solo");
    options.RoutePrefix = "";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


