using GymProgress.Api.Interface;
using GymProgress.Api.MongoHelpers;
using GymProgress.Api.Service;
using GymProgress.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IExerciceService, ExerciceService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISeanceService, SeanceService>();
builder.Services.AddTransient<ISetDataService, SetDataService>();
builder.Services.AddTransient<MongoHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
