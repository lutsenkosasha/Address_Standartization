using AddressStandardization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавление HttpClient с базовым URL для Dadata API
builder.Services.AddHttpClient("DadataClient", client =>
{
    client.BaseAddress = new Uri("https://dadata.ru/api/v2/clean/address");
});

// Регистрация DadataService
builder.Services.AddScoped<DadataService>();

// Остальные настройки
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Address Standardization API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Address Standardization API v1");
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthorization();
app.MapControllers();
app.Run();