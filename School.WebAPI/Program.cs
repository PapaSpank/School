using Microsoft.OpenApi.Models;
using School.WebAPI.BLL;
using School.WebAPI.DAL;
using School.WebAPI.Filters;
using School.WebAPI.Helpers;
using School.WebAPI.Helpers.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "School.API", Version = "v1" });
    c.OperationFilter<SwaggerFileOperationFilter>();
});

// custom services
builder.Services.AddSingleton<ICsvFileParser, CsvFileParser>();
builder.Services.AddSingleton<IStudentValidation, StudentValidation>();
builder.Services.AddSingleton<IStudentBLL, StudentBLL>();
builder.Services.AddSingleton<IStudentDAL, StudentDAL>();

var app = builder.Build();

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
