using MateMachine.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ICurrencyConverter, CurrencyConverter>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MateMachine Task",
        Contact = new OpenApiContact
        {
            Name = "Behzad Dara",
            Email = "Behzad.Dara.99@gmail.com",
            Url = new Uri("https://www.linkedin.com/in/behzaddara/")
        }
    });
});

var app = builder.Build();

app.MapPost("/ClearConfiguration", 
    (ICurrencyConverter currencyConverter)
    => currencyConverter.ClearConfiguration());

app.MapPost("/UpdateConfiguration", 
    (ICurrencyConverter currencyConverter,
    [FromBody] IEnumerable<Tuple<string, string, double>> conversionRates)
    => currencyConverter.UpdateConfiguration(conversionRates));

app.MapGet("/Convert", 
    (ICurrencyConverter currencyConverter,
    string fromCurrency, string toCurrency, double amount)
    => currencyConverter.Convert(fromCurrency, toCurrency, amount));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
