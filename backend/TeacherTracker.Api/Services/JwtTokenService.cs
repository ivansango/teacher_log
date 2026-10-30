
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using TeacherTracker.Api.Models;

namespace TeacherTracker.Api.Services;

public class JwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(ApplicationUser user)
    {
        var issuer = _configuration["Jwt:Issuer"]
            ?? throw new InvalidOperationException(
                "JWT Issuer nije konfiguriran.");

        var audience = _configuration["Jwt:Audience"]
            ?? throw new InvalidOperationException(
                "JWT Audience nije konfiguriran.");

        var keyBase64 = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException(
                "JWT Key nije konfiguriran.");

        var keyBytes = Convert.FromBase64String(keyBase64);

        if (keyBytes.Length < 32)
        {
            throw new InvalidOperationException(
                "JWT ključ mora imati najmanje 32 bajta.");
        }

        var securityKey = new SymmetricSecurityKey(keyBytes);

        var credentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha256);

        var now = DateTime.UtcNow;

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),

            new(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()),

            new(JwtRegisteredClaimNames.Iat,
                EpochTime.GetIntDate(now).ToString(),
                ClaimValueTypes.Integer64)
        };

        if (!string.IsNullOrWhiteSpace(user.Email))
        {
            claims.Add(new Claim(
                JwtRegisteredClaimNames.Email,
                user.Email));
        }

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = issuer,
            Audience = audience,
            IssuedAt = now,
            NotBefore = now,
            Expires = now.AddMinutes(15),
            SigningCredentials = credentials
        };

        var handler = new JsonWebTokenHandler();

        return handler.CreateToken(descriptor);
    }
}
