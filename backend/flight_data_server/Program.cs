using flight_data_server.Database;
using flight_data_server.Interface;
using flight_data_server.Services.DBFunctions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using flight_data_server.Services.UserFunctions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddScoped<IAirlinerDBFunctions, AirlinerDBFunctions>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFlightDataDBFunctions, FlightDataDBFunctions>();
builder.Services.AddScoped<IFlightDataInfoFunctions, FlightDataInfoFunctions>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient();


var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = true;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
            {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
            };
    });



var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.

builder.Services.AddDbContext<AirlinerDBContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("FlightDB"));
});


builder.Services.AddDbContext<FlightDataDBContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("FlightDB"));
});


builder.Services.AddDbContext<UserDBContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("FlightDB"));
});


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*");
                      });
});



builder.Services.AddSwaggerGen(

    options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
            Description =
                "JWT Authorization header using the Bearer scheme. \r\n\r\n "
                + "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n"
                + "Example: \"Bearer 12345abcdef\"",
            Name = "Authorization",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Scheme = "Bearer"
            });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

}
);

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment())
    {
    app.UseSwagger();
    app.UseSwaggerUI();
    IdentityModelEventSource.ShowPII = true;
    }

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyHeader()
           .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS");
});

app.Run();
