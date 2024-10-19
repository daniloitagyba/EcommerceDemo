using Ordering.Domain.AggregatesModel.BuyerAggregate;
using Ordering.Domain.SeedWork;

namespace Ordering.API.Infrastructure
{
    public class OrderingContextSeed : IDbSeeder<OrderingContext>
    {
        public async Task SeedAsync(OrderingContext context)
        {

            if (!context.CardTypes.Any())
            {
                context.CardTypes.AddRange(GetPredefinedCardTypes());

                await context.SaveChangesAsync();
            }

            await context.SaveChangesAsync();
        }

        private static IEnumerable<CardType> GetPredefinedCardTypes()
        {
            return Enumeration.GetAll<CardType>();
        }
    }
}
