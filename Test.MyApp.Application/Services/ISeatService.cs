using Test.MyApp.Domain.Common;
using Test.MyApp.Domain.DTO.Request;
using Test.MyApp.Domain.DTO.Response;
using Test.MyApp.Domain.EntityModels;

namespace Test.MyApp.Application.Services
{
    public interface ISeatService
    {
        Task<PagingResult<GetSeatResponse>> GetPagination(Expression<Func<Seat, bool>>? predicate, int page, int size);
        Task<Result<GetSeatResponse>> GetById(int id);
        Task<Result<GetSeatResponse>> Create(CreateSeatRequest request);
        Task<Result<bool>> Update(int id, UpdateSeatRequest request);
        Task<Result<bool>> Delete(int id);
    }
}
