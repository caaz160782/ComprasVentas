using System;
using System.Security.Claims;
using ComprasVentas.Models;

namespace ComprasVentas.Services.specification;

public interface ITokenService
{
    string GenerateToken (Usuario usuario);

    string GenerateRefreshToken();

    DateTime GetTokenExpiration();
    
    ClaimsPrincipal ? GetPrincipalFromExpiredToken (string token);

}

