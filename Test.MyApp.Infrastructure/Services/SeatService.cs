using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Test.MyApp.Application.Repositories;
using Test.MyApp.Application.Services;
using Test.MyApp.Domain.Common;
using Test.MyApp.Domain.DTO.Request;
using Test.MyApp.Domain.DTO.Response;
using Test.MyApp.Domain.EntityModels;
using Test.MyApp.Domain.Pagination;

namespace Test.MyApp.Infrastructure.Services
{
    public class SeatService(IUnitOfWork<MyAppDbContext> unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
       : BaseService<AuthenticateService>(unitOfWork, mapper, httpContextAccessor), ISeatService
    {
        public async Task<Result<GetSeatResponse>> Create(CreateSeatRequest request)
        {
            try
            {
                var seatColor = await _unitOfWork.GetRepository<SeatColor>().GetAsync(predicate: i => i.Id == request.SeatColorId) ?? throw new Exception("NotFoundColor(1 or 2)");
                Seat seat = request.Adapt<Seat>();
                var result = await _unitOfWork.GetRepository<Seat>().InsertAsync(seat);
                bool success = await _unitOfWork.CommitAsync() > 0;
                if (!success)
                {
                    throw new Exception("FailCreateSeat");
                }

                return Success(result.Adapt<GetSeatResponse>());
            }
            catch (Exception ex)
            {
                return Fail<GetSeatResponse>(ex.Message);
            }
        }

        public async Task<Result<bool>> Delete(int id)
        {
            try
            {
                Seat seat = await _unitOfWork.GetRepository<Seat>().GetAsync(
                       predicate: a => a.Id.Equals(id), include: x => x.Include(its => its.SeatColor));
                if (seat == null)
                {
                    throw new Exception("NotFoundSeat");
                }
                _unitOfWork.GetRepository<Seat>().DeleteAsync(seat);
                await _unitOfWork.CommitAsync();
                return Success<bool>(true);
            }
            catch (Exception ex)
            {
                return Fail<bool>(ex.Message);
            }
        }

        public async Task<Result<GetSeatResponse>> GetById(int id)
        {

            try
            {
                Seat seat = await _unitOfWork.GetRepository<Seat>().GetAsync(
                                   predicate: a => a.Id.Equals(id), include: x => x.Include(its => its.SeatColor));
                if (seat == null)
                {
                    throw new Exception("NotFoundSeat");
                }
                return Success(seat.Adapt<GetSeatResponse>());
            }
            catch (Exception ex)
            {
                return BadRequest<GetSeatResponse>(ex.Message);
            }
        }

        public async Task<PagingResult<GetSeatResponse>> GetPagination(Expression<Func<Seat, bool>>? predicate, int page, int size)
        {
            try
            {
                IPaginate<Seat> seat = await _unitOfWork.GetRepository<Seat>().GetPagingListAsync(include: x => x.Include(its => its.SeatColor),
                    page: page,
                    size: size);
                return SuccessWithPaging(
                    seat.Adapt<IPaginate<GetSeatResponse>>(),
                    page,
                    size,
                    seat.Total
                );
            }
            catch (Exception ex)
            {

            }
            return null!;
        }

        public async Task<Result<bool>> Update(int id, UpdateSeatRequest request)
        {

            try
            {
                Seat? seat = await _unitOfWork.GetRepository<Seat>().GetAsync(
                    predicate: x => x.Id.Equals(id));
                if (seat == null)
                {
                    throw new Exception("NotFoundSeat");
                }
                var seatColor = await _unitOfWork.GetRepository<SeatColor>().GetAsync(predicate: i => i.Id == request.SeatColorId) ?? throw new Exception("NotFoundColor(1 or 2)");    
                seat.RowChar = request.RowChar ?? seat.RowChar;
                seat.SeatColorId = request.SeatColorId ?? seat.SeatColorId;
                seat.ColNumber = request.ColNumber ?? seat.ColNumber;
                seat. Floor= request.Floor ?? seat.Floor;
                _unitOfWork.GetRepository<Seat>().UpdateAsync(seat);
                bool success = await _unitOfWork.CommitAsync() > 0;
                if (!success)
                {
                    throw new Exception("FailUpdateSeat");
                }

                return Success(success);
            }
            catch (Exception ex)
            {
                return Fail<bool>(ex.Message);
            }
        }
    }
}
