using Microsoft.EntityFrameworkCore;
using Test.MyApp.Domain.Pagination;

namespace Test.MyApp.Infrastructure.Ultils
{
    public static class PaginateQueryable
    {
        public static async Task<IPaginate<T>> ToPaginateAsync<T>(this IQueryable<T> query, int pageNumber,
            int pageSize, int firstPage = 1)
        {
            if (firstPage > pageNumber)
                throw new ArgumentException($"Page ({pageNumber}) must greater or equal than firstPage ({firstPage})");
            var total = await query.CountAsync();
            return new Paginate<T>
            {
                Items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(),
                Page = pageNumber,
                Size = pageSize,
                Total = await query.CountAsync(),
                TotalPages = (int)Math.Ceiling(total / (double)pageSize)
            };
        }
    }
}
