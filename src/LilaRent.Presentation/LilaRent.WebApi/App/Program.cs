using LilaRent.Application;
using LilaRent.Application.DependencyInjection;

//using LilaRent.DataBase.DependencyInjection;
using LilaRent.Dapper.DependencyInjection;

using LilaRent.Domain.Interfaces;
using LilaRent.WebApi;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.AddJwtConfiguration();


builder.Services.AddDtoMapper();
builder.Services.AddDtoValidators();
builder.Services.AddHashingServices();
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddUseCases();
builder.Services.AddUnitOfWork();
builder.Services.AddTransient<IFileService, FileService>();


builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false));
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseStaticFiles();

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
