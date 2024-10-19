using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Infrastructure.Idempotency;

namespace Ordering.Infrastructure.EntityConfigurations
{
    public class ClientRequestEntityTypeConfiguration
    : IEntityTypeConfiguration<ClientRequest>
    {
        public void Configure(EntityTypeBuilder<ClientRequest> requestConfiguration)
        {
            requestConfiguration.ToTable("requests");
        }
    }
}
