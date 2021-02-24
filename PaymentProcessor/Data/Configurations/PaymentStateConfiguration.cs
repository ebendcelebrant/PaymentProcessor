using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentProcessor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentProcessor.Data.Configurations
{
    public class PaymentStateConfiguration : BaseConfiguration<PaymentState>
    {
        public override void Configure(EntityTypeBuilder<PaymentState> modelBuilder)
        {

            modelBuilder.Property(x => x.State)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsRequired(false);

            modelBuilder.ToTable("Payments");
        }
    }
}
