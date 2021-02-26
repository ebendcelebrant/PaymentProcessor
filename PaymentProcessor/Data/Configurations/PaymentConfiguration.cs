using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentProcessor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProcessor.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> modelBuilder)
        {
            modelBuilder.Property(x => x.SecurityCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsRequired(false);

            modelBuilder.Property(x => x.CreditCardNumber)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Property(x => x.CardHolder)
                .IsUnicode(false)
                .IsRequired();

            modelBuilder.Property(x => x.ExpirationDate)
                .HasColumnType("DATETIME")
                .IsRequired();

            modelBuilder.Property(x => x.Amount)
                .HasPrecision(19,4)
                .IsRequired();

            modelBuilder.ToTable("Payments");
            modelBuilder.HasOne(x => x.PaymentState)
                .WithOne(x => x.Payment)
                .HasForeignKey<PaymentState>(x => x.Id);


        }
    }
}
