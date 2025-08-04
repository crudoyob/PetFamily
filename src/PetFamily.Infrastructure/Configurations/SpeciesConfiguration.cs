using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.SpeciesAggregate;

namespace PetFamily.Infrastructure.Configurations;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");
        
        builder.HasKey(s => s.Id)
            .HasName("pk_species");
        
        builder.Property(s => s.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => SpeciesId.Create(value));

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(LengthConstants.LENGTH100)
            .HasColumnName("name");

        builder.HasMany(s => s.Breeds)
            .WithOne()
            .HasForeignKey("species_id")
            .HasConstraintName("fk_breeds_species_species_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}