using MapsterMapper;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Claims;
using Test.MyApp.Application.Repositories;
using Test.MyApp.Domain.Common;
using Test.MyApp.Domain.Pagination;

namespace Test.MyApp.Infrastructure.Services
{
    public abstract class BaseService<T> where T : class
    {
        protected IUnitOfWork _unitOfWork;
        protected IMapper _mapper;
        protected IHttpContextAccessor _httpContextAccessor;
        public BaseService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected Result<TEntity> Success<TEntity>(TEntity entity) => new Result<TEntity>
        {
            Data = entity,
            StatusCode = HttpStatusCode.OK
        };

        protected Result<TEntity> Fail<TEntity>(string message) => new Result<TEntity>
        {
            Message = message,
            StatusCode = HttpStatusCode.InternalServerError
        };

        protected Result<TEntity> BadRequest<TEntity>(string message) => new Result<TEntity>
        {
            Message = message,
            StatusCode = HttpStatusCode.BadRequest
        };

        protected Result<TEntity> NotFound<TEntity>(string message) => new Result<TEntity>
        {
            Message = message,
            StatusCode = HttpStatusCode.NotFound
        };

        protected PagingResult<TEntity> SuccessWithPaging<TEntity>(IPaginate<TEntity> data, int page, int size, int total)
        {
            return new PagingResult<TEntity>
            {
                Data = data,
                StatusCode = HttpStatusCode.OK,
                PageNumber = page,
                PageSize = size,
                TotalCount = total
            };
        }

    }
}