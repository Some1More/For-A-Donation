using For_A_Donation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using For_A_Donation.Helpers;
using For_A_Donation.Infrastructure.Data;
using For_A_Donation.Services.Business;
using For_A_Donation.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(x =>
            x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<Context>(option => option.UseSqlite("Data Source = For-A-Donation.db"));
builder.Services.AddDbContext<Context>(option => option.UseNpgsql(
"Server=127.0.0.1; User Id=postgres; Password=admin; Port=5432; Database=For-A-Donation_PostreSQL.db"));

builder.Services.AddScoped<IFamilyService, FamilyService>();
builder.Services.AddScoped<IProgressService, ProgressService>();
builder.Services.AddScoped<IPurposeService, PurposeService>();
builder.Services.AddScoped<IRewardService, RewardService>();
builder.Services.AddScoped<ITaskServicecs, TaskService>();
builder.Services.AddScoped<IUserProgressService, UserProgressService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWishService, WishService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<AuthenticationMiddleware>();

app.MapControllers();

app.Run();