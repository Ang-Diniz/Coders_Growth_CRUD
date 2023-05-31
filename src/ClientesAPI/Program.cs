using Dominio;
using FluentValidation;
using Infraestrutura;

var builder = WebApplication.CreateBuilder(args);
var AllowSpecificOrigins = "_allowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSpecificOrigins,
                      policy =>
                      {
                      policy.AllowAnyOrigin();
                      });
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICliente, RepositorioClienteLinq2DB>();
builder.Services.AddScoped<IValidator<Cliente>, ClienteFluentValidation>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors(AllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();