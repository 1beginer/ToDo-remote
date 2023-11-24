using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDo.Api;
using ToDo.Api.Context;
using ToDo.Api.Context.Models;
using ToDo.Api.Extensions;
using ToDo.Api.Repository;
using ToDo.Api.Service;
using ToDo.Api.Service.ServiceImpl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var configration = builder.Configuration;
//添加DbContext以及仓储
builder.Services.AddDbContext<MyToDoContext>(opt =>
{
    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    string connStr = configration.GetConnectionString("ToDoConnection");
    opt.UseSqlServer(connStr);
}).AddUnitOfWork<MyToDoContext>()
.AddCustomRepository<ToDoE, TodoERepository>()
.AddCustomRepository<Memo, MemoRepository>()
.AddCustomRepository<User, UserRepository>();

//添加autoMapper

var automapperConfig = new MapperConfiguration(config =>
{
    config.AddProfile(new AutoMapperProFile());
});
builder.Services.AddSingleton(automapperConfig.CreateMapper());


builder.Services.AddScoped<IToDoService, ToDoService>();
builder.Services.AddScoped<IMemoService, MemoService>();
builder.Services.AddScoped<ILoginService, LoginService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
