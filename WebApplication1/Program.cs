using Microsoft.AspNetCore.Hosting;
using PdfSharp.Charting;
using TennisCourtBookingApp.Provider.IProvider;
using TennisCourtBookingApp.Provider.Provider;
using AutoMapper;
using TennisCourtBookingApp.Provider.Mapping;
using TennisCourtBookingApp.Provider;
using TennisCourtBookingApp.Common.Utility;
using TennisCourtBookingApp.Repository.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddProviderServices(builder.Configuration);
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddSession();

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
