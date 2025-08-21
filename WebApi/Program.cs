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
builder.Services.ConfigureActionFilters(); // action filterlarý ekledik
builder.Services.ConfigureCors(); // CORS ayarlarýný ekledik
// bu konfigurasonu yaptýðýmýzda apimiz sadece json üzerinden deðil farklý media type'lar üzerinden de içerik alýp gönderebilir.
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
})
.AddCustomCsvFormatter()
.AddXmlDataContractSerializerFormatters() // xml media type'ý için ekleme yaptýk
.AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
.AddNewtonsoftJson();


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;  // model statesi valid olmadýðýnda hata kodlarýnýn daha detaylý olmasýný saðlar. sadece 400 dönmez 402 gibi detay verir
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


var app = builder.Build();

// logger service kullanýmý
var logger = app.Services.GetRequiredService<ILoggerService>(); // ihtiyac duyulan serviceyi bu þekilde alabildik
app.ConfigureExceptionHandler(logger); // webapplication için yazdýðýmýz eklenti metoda parametre olarak geçtik

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
