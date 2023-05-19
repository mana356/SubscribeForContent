using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using SFC_DataAccess.Data;
using SubscribeForContentAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SFCDBContext>(options =>
{    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnectionString"));

});
builder.Services.InitializeAppServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddSingleton(provider => new BlobServiceClient(builder.Configuration.GetConnectionString("BlobStorage")));

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
