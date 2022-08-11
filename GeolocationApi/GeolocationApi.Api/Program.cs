using GeolocationApi.Api.MIddlewares;
using GeolocationApi.Application;
using GeolocationApi.Persistence.EF;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGeolocationApi(builder.Configuration);
builder.Services.AddGeolocationEF(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
