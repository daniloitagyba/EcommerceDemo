﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.AggregatesModel.BuyerAggregate;

namespace Ordering.Infrastructure.EntityConfigurations
{
    public class PaymentMethodEntityTypeConfiguration
    : IEntityTypeConfiguration<PaymentMethod>
    {
        public void Configure(EntityTypeBuilder<PaymentMethod> paymentConfiguration)
        {
            paymentConfiguration.ToTable("paymentmethods");

            paymentConfiguration.Ignore(b => b.DomainEvents);

            paymentConfiguration.Property(b => b.Id)
                .UseHiLo("paymentseq");

            paymentConfiguration.Property<int>("BuyerId");

            paymentConfiguration
                .Property("_cardHolderName")
                .HasColumnName("CardHolderName")
                .HasMaxLength(200);

            paymentConfiguration
                .Property("_alias")
                .HasColumnName("Alias")
                .HasMaxLength(200);

            paymentConfiguration
                .Property("_cardNumber")
                .HasColumnName("CardNumber")
                .HasMaxLength(25)
                .IsRequired();

            paymentConfiguration
                .Property("_expiration")
                .HasColumnName("Expiration")
                .HasMaxLength(25);

            paymentConfiguration
                .Property("_cardTypeId")
                .HasColumnName("CardTypeId");

            paymentConfiguration.HasOne(p => p.CardType)
                .WithMany()
                .HasForeignKey("_cardTypeId");
        }
    }
}
