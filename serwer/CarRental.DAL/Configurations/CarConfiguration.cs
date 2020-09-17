using CarRental.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRental.DAL.Configurations
{
    class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.Property(p => p.Brand).HasMaxLength(30).IsRequired();
            builder.Property(p => p.Model).HasMaxLength(20).IsRequired();
            builder.Property(p => p.RegistrationNumber).HasMaxLength(7).IsRequired();
            builder.Property(p => p.NumberOfDoor).HasMaxLength(1).IsRequired();
            builder.Property(p => p.NumberOfSits).HasMaxLength(1).IsRequired();
            builder.Property(p => p.YearOfProduction).HasMaxLength(4).IsRequired();
        }
    }
}
