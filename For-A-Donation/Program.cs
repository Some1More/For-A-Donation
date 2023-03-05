using For_A_Donation.Models.DataBase;
using For_A_Donation.Services;
using For_A_Donation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(x =>
            x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(option => option.UseSqlite("Data Source = For-A-Donation.db"));

builder.Services.AddScoped<IFamilyService, FamilyService>();
builder.Services.AddScoped<IProgressService, ProgressService>();
builder.Services.AddScoped<IRewardService, RewardService>();
builder.Services.AddScoped<ITaskServicecs, TaskService>();
builder.Services.AddScoped<IUserProgressService, UserProgressService>();
builder.Services.AddScoped<IUserService, UserService>();

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