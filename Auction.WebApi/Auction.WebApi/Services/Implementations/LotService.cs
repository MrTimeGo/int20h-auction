using Auction.WebApi.Data;
using Auction.WebApi.Dto;
using Auction.WebApi.Dto.Lot;
using Auction.WebApi.Entities;
using Auction.WebApi.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Auction.WebApi.Services.Implementations;

public class LotService(AuctionContext context, ICurrentUserService currentUserService, IMapper mapper) : ILotService
{
    public async Task<PaginationResult<LotDto>> GetLotsAsync(string? searchTerm, LotFilter? filter, LotSort? sort, PaginationModel? pagination)
    {
        var query = context.Lots.AsQueryable();

        if (filter is not null)
        {
            query = ApplyFilter(query, filter);
        }

        if (searchTerm is not null)
        {
            query = ApplySearchTerm(query, searchTerm);
        }

        if (sort is not null)
        {
            query = ApplySort(query, sort);
        }

        var count = await query.CountAsync();

        if (pagination is not null && pagination.PageSize != 0)
        {
            query = query.Skip(pagination.Page * pagination.PageSize).Take(pagination.PageSize);
        }

        var result = await query.ToListAsync();

        return new PaginationResult<LotDto>()
        {
            Entities = mapper.Map<List<LotDto>>(result)!,
            Count = count
        };
    }

    private IQueryable<Lot> ApplyFilter(IQueryable<Lot> query, LotFilter filter)
    {
        query = query
                .Include(l => l.Bets)
                .Where(l => (filter.MyLots && l.AuthorId == currentUserService.CurrentUserId) ||
                    (filter.MyBets && l.Bets.Any(b => b.AuthorId == currentUserService.CurrentUserId))
        );
        if (filter.LotStatus is not null)
        {
            query = query.Where(l => (filter.LotStatus == LotStatus.Active && l.StartingAt < DateTime.UtcNow && DateTime.UtcNow < l.ClosingAt) ||
                    (filter.LotStatus == LotStatus.NotStarted && DateTime.UtcNow < l.StartingAt) ||
                    (filter.LotStatus == LotStatus.Closed && l.ClosingAt < DateTime.UtcNow)
                );
        }

        return query;
    }

    private IQueryable<Lot> ApplySearchTerm(IQueryable<Lot> query, string searchTerm)
    {
        var subterms = searchTerm.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return query.Include(l => l.Tags)
                .Where(l =>
                    subterms.Any(s => l.Name.ToLower().Contains(s)) ||
                    l.Tags.Any(t => subterms.Contains(t.Name.ToLower()))
                );
    }

    private IQueryable<Lot> ApplySort(IQueryable<Lot> query, LotSort sort)
    {
        if (sort.SortOrder == LotSortOrder.Ascending)
        {
            return query.OrderBy(l => sort.Type == LotSortType.StartingAt ? l.StartingAt : l.ClosingAt);
        }
        else
        {
            return query.OrderByDescending(l => sort.Type == LotSortType.StartingAt ? l.StartingAt : l.ClosingAt);
        }
    }
}
