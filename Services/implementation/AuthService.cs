using System;
using ComprasVentas.Common;
using ComprasVentas.Data;
using ComprasVentas.Dto.auth;
using ComprasVentas.Exceptions;
using ComprasVentas.Models;
using ComprasVentas.Services.specification;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ComprasVentas.Services.implementation;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;

    private readonly AppDbContext _appDbContext;
    
    private readonly JwtSettings _jwtSettings;
    public AuthService(AppDbContext context, 
                       ITokenService tokenService,
                       IOptions<JwtSettings> jwtSettings)
    {
        _appDbContext = context;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;

    }
    
    public async Task<AuthResponseDto> AuthenticateAsync(AuthRequestDto authRequestDto)
    {
        var user = await _appDbContext.Usuarios
                        .Include(r=>r.Roles)
                        .ThenInclude(r=> r.Permisos)
                        .FirstOrDefaultAsync(u => u.Nombre == authRequestDto.Username);
        if (user == null || user.Password != authRequestDto.Password)
        {
            throw new BadRequestException("credenciales no validas");
        }
        var accessToken = _tokenService.GenerateToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            Token   = refreshToken,
            Expires = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays),
            UsuarioId = user.Id,
            Created= DateTime.UtcNow
        };

        _appDbContext.RefreshTokens.Add(refreshTokenEntity);
        await _appDbContext.SaveChangesAsync();

        return new AuthResponseDto
        {
            Token= accessToken,
            RefreshToken= refreshToken,
            Expiration= DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
            identifier= user.Id
        };
    }

    public Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        throw new NotImplementedException();
    }
}
