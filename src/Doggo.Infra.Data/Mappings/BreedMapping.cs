using Doggo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doggo.Infra.Data.Mappings.Parametrizacao
{
    public class BreedMapping : IEntityTypeConfiguration<Breed>
    {
        public void Configure(EntityTypeBuilder<Breed> builder)
        {
            builder.ToTable("Breeds");
            builder.HasKey(c => c.Id).IsClustered(true);
            builder.HasIndex(c => c.UniqueId).IsUnique();

            builder.Property(c => c.Id).UseIdentityColumn();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Type).IsRequired();
            builder.Property(c => c.Family).IsRequired();
            builder.Property(c => c.Origin).IsRequired();
            builder.Property(c => c.OtherNames).IsRequired();
        }
    }
}
