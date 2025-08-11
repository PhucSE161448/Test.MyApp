using Azure.Core;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.MyApp.Application.Repositories;
using Test.MyApp.Application.Services;
using Test.MyApp.Domain.Common;
using Test.MyApp.Domain.DTO.Request;
using Test.MyApp.Domain.DTO.Response;
using Test.MyApp.Domain.EntityModels;
using Test.MyApp.Infrastructure.Ultils;

namespace Test.MyApp.Infrastructure.Services
{
    public class AuthenticateService(IUnitOfWork<MyAppDbContext> unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
       : BaseService<AuthenticateService>(unitOfWork, mapper, httpContextAccessor), IAuthenticateService
    {
        public async Task<Result<TokenResponse?>> LoginAsync(LoginRequest loginRequest)
        {
            try
            {
                var user = await _unitOfWork.GetRepository<User>()
                .GetAsync(predicate: a => a.Email.Equals(loginRequest.Email) && a.PasswordHash.Equals(loginRequest.Password));

                if (user == null)
                {
                    return null;
                }

                var response = new TokenResponse()
                {
                    AccessToken = Jwt.GenerateJwtToken(user),
                    RefreshToken = Jwt.GenerateRefreshToken(),
                };
                return Success(response);
            }
            catch (Exception ex)
            {
                return Fail<TokenResponse>(ex.Message);
            }
        }

        public async Task<Result<TokenResponse?>> RefreshTokenAsync(string refreshToken)
        {
            var user = await _unitOfWork.GetRepository<User>().GetAsync(predicate: u => u.RefreshToken == refreshToken);

            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new Exception("Don't have user with this refresh token");

            var newAccessToken = Jwt.GenerateJwtToken(user);
            var newRefreshToken = Jwt.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            user.CreatedAt = DateTime.UtcNow;
            _unitOfWork.GetRepository<User>().UpdateAsync(user);
            await _unitOfWork.CommitAsync();

            return Success(new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}
