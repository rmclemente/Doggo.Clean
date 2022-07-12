using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.EntitiesConfiguration
{
    public class BreedConfiguration : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).UseIdentityAlwaysColumn();
            builder.HasIndex(p => p.ExternalId);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Family)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Origin)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.BreedType)
                .HasConversion(p => p.Id, p => BreedType.FromId(p))
                .IsRequired();
        }
    }
}
