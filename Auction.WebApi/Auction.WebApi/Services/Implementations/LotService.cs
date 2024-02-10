using Auction.WebApi.Data;
using Auction.WebApi.Dto;
using Auction.WebApi.Dto.Lot;
using Auction.WebApi.Entities;
using Auction.WebApi.Services.Interfaces;
using Auction.WebApi.Expections;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Auction.WebApi.Dto.Bet;

namespace Auction.WebApi.Services.Implementations;

public class LotService(AuctionContext context, ICurrentUserService currentUserService, IMapper mapper) : ILotService
{
    public async Task<LotDto> CreateLotAsync(CreateLotDto dto)
    {
        var entity = mapper.Map<Lot>(dto)!;

        entity.AuthorId = currentUserService.CurrentUserId!.Value;

        entity.Images = await context.StaticFiles.Where(sf => dto.Images.Contains(sf.Id)).ToListAsync();
        

        var existingTags = await context.Tags.Where(x => dto.Tags.Any(y => x.Name.ToLower() == y.ToLower())).ToListAsync();
        var notExistingTags = dto.Tags.Where(x => !existingTags.Any(y => y.Name == x)).ToList();

        if (notExistingTags.Count != 0)
        {
            var newTags = await CreateTags(notExistingTags);
            existingTags.AddRange(newTags);
        }

        entity.Tags = existingTags;

        var result = context.Lots.Add(entity);

        await context.SaveChangesAsync();

        return mapper.Map<LotDto>(result.Entity)!;
    }

    private async Task<List<Tag>> CreateTags(List<string> tags)
    {
        context.Tags.AddRange(tags.Select(t => new Tag { Name = t }));

        await context.SaveChangesAsync();

        return await context.Tags.Where(t => tags.Contains(t.Name)).ToListAsync();
    }

    public async Task<PaginationResult<LotDto>> GetLotsAsync(string? searchTerm, LotFilter filter, LotSort sort, PaginationModel pagination)
    {
        var query = context.Lots.Include(l => l.Tags).AsQueryable();

        if (filter.MyLots is not null || filter.MyBets is not null || filter.LotStatus is not null)
        {
            query = ApplyFilter(query, filter);
        }

        if (searchTerm is not null)
        {
            query = ApplySearchTerm(query, searchTerm);
        }

        if (sort.Type is not null)
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
                .Where(l => (filter.MyLots!.Value && l.AuthorId == currentUserService.CurrentUserId) ||
                    (filter.MyBets!.Value && l.Bets.Any(b => b.AuthorId == currentUserService.CurrentUserId))
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

        return query
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

    public async Task<LotDetailedDto> GetLotByIdAsync(Guid id)
    {
        var lot = await context.Lots
            .Include(l => l.Tags)
            .Include(l => l.Bets.OrderByDescending(b => b.Amount))
            .Include(l => l.Images)
            .ProjectTo<LotDetailedDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(l => l.Id == id);

        return lot ?? throw new NotFoundExeption($"Lot with id {id} not found");
    }

    public async Task MakeBet(Guid lotId, MakeBetDto dto)
    {
        var lot = await context.Lots.FirstOrDefaultAsync(l => l.Id == lotId);

        if (lot is null)
        {
            throw new NotFoundExeption($"Lot with id {lotId} not found");
        }

        var latestBet = await context.Bets.Where(b => b.LotId == lot.Id).OrderByDescending(b => b.Amount).FirstOrDefaultAsync();

        if (dto.Amount < (lot.InitialPrice + lot.MinimalStep))
        {
            throw new ConfictExeption("Amount of new bet is lesser then initial price + minimal step");
        }

        if (latestBet is not null && (latestBet.Amount + lot.MinimalStep) > dto.Amount)
        {
            throw new ConfictExeption("Amount of new bet is lesser then latest amount + minimal step");
        }

        var bet = new Bet()
        {
            AuthorId = currentUserService.CurrentUserId!.Value,
            Amount = dto.Amount,
            LotId = lot.Id
        };

        context.Bets.Add(bet);
        await context.SaveChangesAsync();
    }

    public async Task<LotDto> UpdateLotAsync(Guid id, CreateLotDto dto)
    {
        var existingEntity = await context.Lots.FirstOrDefaultAsync(l => l.Id == id);

        if (existingEntity is null)
        {
            throw new NotFoundExeption($"Lot with id {id} not found");
        }

        if (existingEntity.StartingAt < DateTime.UtcNow)
        {
            throw new BadRequestException($"Can't update lot after its beginning");
        }

        existingEntity.Name = dto.Name;
        existingEntity.Description = dto.Description;
        existingEntity.InitialPrice = dto.InitialPrice;
        existingEntity.MinimalStep = dto.MinimalStep;
        existingEntity.StartingAt = dto.StartingAt;
        existingEntity.ClosingAt = dto.ClosingAt;


        existingEntity.Images = await context.StaticFiles.Where(sf => dto.Images.Contains(sf.Id)).ToListAsync();


        var existingTags = await context.Tags.Where(x => dto.Tags.Any(y => x.Name.ToLower() == y.ToLower())).ToListAsync();
        var notExistingTags = dto.Tags.Where(x => !existingTags.Any(y => y.Name == x)).ToList();

        if (notExistingTags.Count != 0)
        {
            var newTags = await CreateTags(notExistingTags);
            existingTags.AddRange(newTags);
        }

        existingEntity.Tags = existingTags;

        var result = context.Lots.Update(existingEntity);

        await context.SaveChangesAsync();

        return mapper.Map<LotDto>(result.Entity)!;
    }
}
