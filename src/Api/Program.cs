using Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
//builder.AddSerilogConfiguration();
builder.AddApiServices();
builder.AddSwaggerService();
builder.AddRegisteredServices();

var app = builder.Build();
// Configure the HTTP request pipeline.
//app.UseSerilogConfiguration();
app.UseApiServices();
app.UseSwaggerService();
app.Run();
