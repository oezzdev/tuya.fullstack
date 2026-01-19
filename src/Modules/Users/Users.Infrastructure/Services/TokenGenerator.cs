using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Users.Application.Queries.GetUserByCredentials;
using Users.Infrastructure.Options;

namespace Users.Infrastructure.Services;

public class TokenGenerator(IOptions<SignatureOptions> options)
{
    public string Generate(GetUserByCredentialsResult user)
    {
        var handler = new JwtSecurityTokenHandler();
        SecurityTokenDescriptor tokenDescriptor = GetTokenDescriptor(user);
        SecurityToken? token = handler.CreateToken(tokenDescriptor);

        return handler.WriteToken(token);
    }

    private SecurityTokenDescriptor GetTokenDescriptor(GetUserByCredentialsResult user)
    {
        var claimsIdentity = new ClaimsIdentity([
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
        ]);

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(options.Value.SymmetricKey));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = DateTimeOffset.UtcNow.Add(options.Value.SignatureLifetime).UtcDateTime,
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        };

        return tokenDescriptor;
    }
}
