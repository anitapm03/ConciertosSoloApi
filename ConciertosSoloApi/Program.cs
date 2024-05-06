using Azure.Security.KeyVault.Secrets;
using ConciertosSoloApi.Data;
using ConciertosSoloApi.Helpers;
using ConciertosSoloApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAzureClients(factory =>
{
    factory.AddSecretClient(
        builder.Configuration.GetSection("KeyVault"));
});

SecretClient secretClient =
    builder.Services.BuildServiceProvider().GetService<SecretClient>();

KeyVaultSecret secret = await
    secretClient.GetSecretAsync("SQL");

//cositas de la seguridad
//creamos una instancia del helper
HelperActionServicesOAuth helper =
    new HelperActionServicesOAuth(builder.Configuration, secretClient);
//esta instancia del helper debemos incluirla dentro
//de nuestra app solamente una vez para que todo lo que
//hemos creado dentro no se genere de nuevo
builder.Services.AddSingleton<HelperActionServicesOAuth>(helper);
//habilitamos los servicios de auth que hemos creado 
//en el helper con action<>
builder.Services.AddAuthentication
    (helper.GetAuthenticationSchema())
    .AddJwtBearer(helper.GetJwtBearerOptions());

// Add services to the container.
string connectionString = secret.Value;
    //builder.Configuration.GetConnectionString("SQL");
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


