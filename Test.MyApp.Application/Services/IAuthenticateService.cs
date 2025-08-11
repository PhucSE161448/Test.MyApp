using Test.MyApp.Domain.Common;
using Test.MyApp.Domain.DTO.Request;
using Test.MyApp.Domain.DTO.Response;

namespace Test.MyApp.Application.Services
{
    public interface IAuthenticateService
    {
        Task<Result<TokenResponse?>> LoginAsync(LoginRequest loginRequest);
        Task<Result<TokenResponse?>> RefreshTokenAsync(string refreshToken);
    }
}
