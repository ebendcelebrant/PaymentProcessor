using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentProcessor.Domain.Models;

namespace PaymentProcessor.Data.Configurations
{
    public class BaseConfiguration<TModel> : IEntityTypeConfiguration<TModel>
        where TModel : BaseModel
    {
        public virtual void Configure(EntityTypeBuilder<TModel> modelBuilder)
        {
            modelBuilder.Property(x => x.DateAdded)
                .HasColumnType("DATETIME")
                .IsRequired();
        }
    }
}
