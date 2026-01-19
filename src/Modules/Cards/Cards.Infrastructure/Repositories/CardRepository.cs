using Cards.Domain.Entities;
using Cards.Domain.Ports;
using Cards.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Cards.Infrastructure.Repositories;

public class CardRepository(CardsContext context) : Repository<Card>(context), ICardRepository
{
    private readonly CardsContext context = context;

    public Task<List<Card>> GetByUser(Guid customerId, CancellationToken cancellationToken = default)
    {
        return context.Cards.Where(card => card.UserId == customerId).ToListAsync(cancellationToken);
    }
}
