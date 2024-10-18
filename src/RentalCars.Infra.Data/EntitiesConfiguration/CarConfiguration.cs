using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCars.Domain.Entities;

namespace RentalCars.Infra.Data.EntitiesConfiguration;

internal class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable(nameof(Car))
               .HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .HasColumnName("id")
               .HasColumnType("uuid")
               .ValueGeneratedNever();

        builder.Property(x => x.Brand)
               .HasColumnName("brand")
               .HasColumnType("varchar(50)")
               .IsRequired();

        builder.Property(x => x.Model)
               .HasColumnName("model")
               .HasColumnType("varchar(50)")
               .IsRequired();

        builder.Property(x => x.Year)
               .HasColumnName("year")
               .HasColumnType("int")
               .IsRequired();

        builder.Property(x => x.Availability)
               .HasColumnName("availability")
               .HasColumnType("boolean")
               .IsRequired();
    }
}
