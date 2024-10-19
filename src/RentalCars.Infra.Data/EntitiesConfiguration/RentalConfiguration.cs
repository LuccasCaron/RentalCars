using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCars.Domain.Entities;

namespace RentalCars.Infra.Data.EntitiesConfiguration;

internal class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.ToTable(nameof(Rental))
              .HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasColumnName("id")
               .HasColumnType("uuid")
               .ValueGeneratedNever();

        builder.HasOne(e => e.Car)
               .WithMany()
               .HasForeignKey(e => e.CarId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.UserId)
               .HasColumnName("userId")
               .HasColumnType("uuid")
               .IsRequired();

        builder.Property(x => x.InitDate)
               .HasColumnName("initDate")
               .HasColumnType("timestamp")
               .IsRequired();

        builder.Property(x => x.FinalDate)
               .HasColumnName("finalDate")
               .HasColumnType("timestamp")
               .IsRequired();

        builder.Property(x => x.AppliedDailyPrice)
               .HasColumnName("appliedDailyPrice")
               .HasColumnType("int")
               .IsRequired();

        builder.Property(x => x.HasPaymentDelay)
               .HasColumnName("hasPaymentDelay")
               .HasColumnType("boolean")
               .IsRequired();

        builder.Property(x => x.FineAmount)
               .HasColumnName("fineAmount")
               .HasColumnType("int")
               .IsRequired();
    }
}
