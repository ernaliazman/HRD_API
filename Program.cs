using hrd_backend.Data;
using hrd_backend.Interface;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "HRD API",
        Description = "API Documentation"
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDbRepo, DbRepo>();
builder.Services.AddScoped<IOrientationRepo, OrientationRepo>();
builder.Services.AddScoped<IEmpTransferRepo, EmpTransferRepo>();
builder.Services.AddScoped<IGeneralRepo, GeneralRepo>();
builder.Services.AddScoped<IOJTRepo, OJTRepo>();
builder.Services.AddScoped<IPRRepo, PRRepo>();
builder.Services.AddScoped<IJDRepo, JDRepo>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("pronethybridservicescorspolicy");

app.UseStaticFiles();


//api Accept any header, method
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.MapControllers();

app.Run();
