using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.AggregatesModel.OrderAggregate;

namespace Ordering.Infrastructure.EntityConfigurations;

public class OrderItemEntityTypeConfiguration
    : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> orderItemConfiguration)
    {
        orderItemConfiguration.ToTable("orderItems");

        orderItemConfiguration.Ignore(b => b.DomainEvents);

        orderItemConfiguration.Property(o => o.Id)
            .UseHiLo("orderitemseq");

        orderItemConfiguration.Property<int>("OrderId");
    }
}
