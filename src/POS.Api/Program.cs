using POS.Api.Extensions;
using POS.Application.Extensions;
using POS.Infraestructure.Extensions;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
// Add services to the container.
var Cors = "Cors";


builder.Services.AddInjectionInfraescture(Configuration);
builder.Services.AddInjectionApplication(Configuration);
builder.Services.AddAthentification(Configuration); // ENABLE AddSwagger EXTENSISONS AuthenticationExtensions

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger(); // ENABLE AddSwagger EXTENSISONS SwaggerExtensions

//configuration Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Cors,
        builder =>
        {
            builder.WithOrigins("*");
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
});

var app = builder.Build();
//use of cord
app.UseCors(Cors);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseWatchDogExceptionLogger();

app.UseHttpsRedirection();

app.UseAuthentication();// USE ATENTIFICATION JWT

app.UseAuthorization();

app.MapControllers();

app.UseWatchDog(configuration =>
{
    configuration.WatchPageUsername = Configuration.GetSection("WatchDog:Username").Value;
    configuration.WatchPagePassword = Configuration.GetSection("WatchDog:Password").Value;
});


app.Run();

public partial class Program { }
