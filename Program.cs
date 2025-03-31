using Microsoft.EntityFrameworkCore;
using StudentManagement.Data;
using StudentManagement.Services;
using System.Reflection;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Exceptions;
using StudentManagement.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// added Db Context
builder.Services.AddDbContext<StudentDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("StudentConnection")));

// Add AutoMapper
//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddAutoMapper(typeof(MappingProfile)); // Or Assembly.GetExecutingAssembly()

// Register services
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add this instead of UseExceptionHandler with RequestDelegate
//app.UseExceptionHandler(); // Uses the registered IExceptionHandler

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
