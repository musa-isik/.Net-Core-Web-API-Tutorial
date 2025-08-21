using Microsoft.AspNetCore.Mvc;
using NLog;
using Presentation.ActionFilters;
using Services.Contracts;
using WebApi.Extensions;
var builder = WebApplication.CreateBuilder(args);

LogManager.Setup()
    .LoadConfigurationFromFile(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureLoggerService();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureActionFilters(); // action filterlar� ekledik
builder.Services.ConfigureCors(); // CORS ayarlar�n� ekledik
// bu konfigurasonu yapt���m�zda apimiz sadece json �zerinden de�il farkl� media type'lar �zerinden de i�erik al�p g�nderebilir.
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
})
.AddCustomCsvFormatter()
.AddXmlDataContractSerializerFormatters() // xml media type'� i�in ekleme yapt�k
.AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
.AddNewtonsoftJson();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;  // model statesi valid olmad���nda hata kodlar�n�n daha detayl� olmas�n� sa�lar. sadece 400 d�nmez 402 gibi detay verir
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


var app = builder.Build();

// logger service kullan�m�
var logger = app.Services.GetRequiredService<ILoggerService>(); // ihtiyac duyulan serviceyi bu �ekilde alabildik
app.ConfigureExceptionHandler(logger); // webapplication i�in yazd���m�z eklenti metoda parametre olarak ge�tik

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
