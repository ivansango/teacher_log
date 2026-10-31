using Microsoft.EntityFrameworkCore;
using TeacherTracker.Api.Data;
using Microsoft.AspNetCore.Identity;
using TeacherTracker.Api.Models;
using TeacherTracker.Api.Services;
using TeacherTracker.Api.Data.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// DATABASE
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention()/*.UseOpenIddict()*/);

//IDENTITY OPTIONS
builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;

    options.User.RequireUniqueEmail = true;

    options.Lockout.DefaultLockoutTimeSpan =
    TimeSpan.FromMinutes(5);

    options.Lockout.MaxFailedAccessAttempts = 5;

    options.Lockout.AllowedForNewUsers = true;
})
.AddRoles<IdentityRole>()
.AddSignInManager()
.AddEntityFrameworkStores<ApplicationDbContext>();

/*
// JWT INITIALIZATION
var jwtIssuer = builder.Configuration["Jwt:Issuer"]
    ?? throw new InvalidOperationException(
        "JWT Issuer nije konfiguriran.");

var jwtAudience = builder.Configuration["Jwt:Audience"]
    ?? throw new InvalidOperationException(
        "JWT Audience nije konfiguriran.");

var jwtKeyBase64 = builder.Configuration["Jwt:Key"]
    ?? throw new InvalidOperationException(
        "JWT Key nije konfiguriran.");

byte[] jwtKey;

try
{
    jwtKey = Convert.FromBase64String(jwtKeyBase64);
}
catch (FormatException)
{
    throw new InvalidOperationException(
        "JWT Key mora biti valjan Base64 niz.");
}

if (jwtKey.Length < 32)
{
    throw new InvalidOperationException(
        "JWT ključ mora imati najmanje 32 bajta.");
}

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MapInboundClaims = false;

        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,

                IssuerSigningKey =
                    new SymmetricSecurityKey(jwtKey),

                ClockSkew = TimeSpan.Zero
            };
    });


builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore()
               .UseDbContext<ApplicationDbContext>();
    });
*/

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<JwtTokenService>();
builder.Services.AddOpenApi();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await DevelopmentDataSeeder.SeedAsync(
        app.Services,
        app.Configuration);

    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

