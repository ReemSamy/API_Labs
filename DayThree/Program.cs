using DayThree.Data.Context;
using DayThree.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
#region Default Services
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region DataBase
var connectionString =builder.Configuration.GetConnectionString("Company");
builder.Services.AddDbContext<CompanyContext>(options =>
{
    options.UseSqlServer(connectionString);
});
#endregion

#region Identity Manager
builder.Services.AddIdentity<Employee, IdentityRole>(options=>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = false;
})
    .AddEntityFrameworkStores<CompanyContext>();

//To Avoid Overriding Anything From Authintication Configuering
#endregion 

#region Configure Authintication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Cool";
    options.DefaultChallengeScheme = "Cool";
})
    .AddJwtBearer("Cool",options=>
    {      //IdentityRole is the Default One
        var keyString = builder.Configuration.GetValue<string>("SecretKey") ?? string.Empty;
        var keyInBytes = Encoding.ASCII.GetBytes(keyString);
        var key = new SymmetricSecurityKey(keyInBytes);
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateActor = false,
        };
    });
#endregion

#region Authurization 
builder.Services.AddAuthorization(options =>

{
    options.AddPolicy("Managers", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "Manager", "CEO")
        .RequireClaim(ClaimTypes.NameIdentifier);
    });
    options.AddPolicy("Employee", policy =>
    {
        policy.RequireClaim(ClaimTypes.Role, "Employee", "CEO","Manager")
        .RequireClaim(ClaimTypes.NameIdentifier);
    });
});



#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();


app.MapControllers();

app.Run();
