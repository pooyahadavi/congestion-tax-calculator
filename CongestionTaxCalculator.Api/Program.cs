using CongestionTaxCalculator.Api.Extensions;
using CongestionTaxCalculator.Api.Middleware;
using CongestionTaxCalculator.Core.General;
using CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Context;
using CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Context.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplicationContext();
builder.Services.AddApplicationIdentity();
builder.Services.AddApplicationAuthentication();
builder.Services.AddApplicationSwagger();
builder.Services.AddApplicationServices();

var app = builder.Build();

app.InitializeDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Congestion Tax Calculator v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ApplicationExceptionMiddleware>();

app.MapControllers();

app.Run();