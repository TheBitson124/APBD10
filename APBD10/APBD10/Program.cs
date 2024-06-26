using APBD10.Controllers;
using APBD10.Data;
using APBD10.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<PrescriptionContext>(
    options => options.UseSqlServer("Name=ConnectionStrings:Default"));
builder.Services.AddScoped<IDbService, DbService>();

var app = builder.Build();

//dotnet tool install --global dotnet-ef
//dotnet ef migrations add Init
//dotnet ef database update

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();



app.Run();

