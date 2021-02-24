using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PaymentProcessor.Domain.Models;
using PaymentProcessor.Data.Configurations;

namespace PaymentProcessor.Data
{
    public class PaymentProcessorContext : DbContext
    {
        public PaymentProcessorContext(DbContextOptions<PaymentProcessorContext> options)
              : base(options)
        {
        }
        public DbSet<PaymentState> PaymentStates { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new PaymentConfiguration());
            builder.ApplyConfiguration(new PaymentStateConfiguration());
        }

        public override int SaveChanges()
        {
            var entities = (from entry in ChangeTracker.Entries()
                            where entry.State == EntityState.Modified || entry.State == EntityState.Added
                            select entry.Entity);

            var validationResults = new List<ValidationResult>();
            foreach (var entity in entities)
            {
                if (!Validator.TryValidateObject(entity, new ValidationContext(entity), validationResults, validateAllProperties: true))
                {
                    throw new ValidationException();
                }
            }
            return base.SaveChanges();
        }
    }
}
