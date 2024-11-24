using App.FieldPermission.Attributes;
using App.FieldPermission.Helpers;
using EfCore.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DapperHelper>();
builder.Services.AddScoped<FieldPermissionService>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<AppDbContext>(o =>
{
    o.UseSqlServer(
        builder.Configuration.GetConnectionString("Default")
    );
});

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