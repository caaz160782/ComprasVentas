using System;
using ComprasVentas.Data;
using ComprasVentas.Dto.auth;
using ComprasVentas.Services.specification;

namespace ComprasVentas.Services.implementation;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;

    private readonly AppDbContext _appDbContext;
    public Task<AuthResponseDto> AuthenticateAsync(AuthRequestDto authRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto refreshTokenDto)
    {
        throw new NotImplementedException();
    }
}
