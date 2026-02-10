using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using ComprasVentas.Common;
using ComprasVentas.Models;
using ComprasVentas.Services.specification;
using Microsoft.IdentityModel.Tokens;

namespace ComprasVentas.Services.implementation;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;

    public TokenService(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }
    
    public string GenerateToken(Usuario usuario)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = System.Text.Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

      var claims = new List<Claim>
      {
          new(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
          new Claim(ClaimTypes.Email, usuario.Correo),
          new Claim(ClaimTypes.Name, usuario.Nombre)
          //add data to claim as needed
      };

      //add roles
      foreach(var role in usuario.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Nombre));
            
            //add permisos
            foreach (var permiso in role.Permisos)
            {
                claims.Add(new Claim("Permiso", permiso.Nombre));    
            }
            
        }

       var tokenDescriptor = new SecurityTokenDescriptor
       {
         Subject = new ClaimsIdentity(claims),
         Expires= DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
         Issuer= _jwtSettings.Issuer,
         Audience=_jwtSettings.Audience,
         SigningCredentials= new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)  
       }; 
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);

    }
    public string GenerateRefreshToken()
    {
       var randomNumber = new byte[32];
       using var rng = RandomNumberGenerator.Create();
       rng.GetBytes(randomNumber);
       return Convert.ToBase64String(randomNumber);
    }


    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        throw new NotImplementedException();
    }

    public DateTime GetTokenExpiration()
    {
        throw new NotImplementedException();
    }
}
