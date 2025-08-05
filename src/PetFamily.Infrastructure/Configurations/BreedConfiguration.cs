using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.SpeciesAggregate.BreedEntity;

namespace PetFamily.Infrastructure.Configurations;

public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breeds");
        
        builder.HasKey(b => b.Id)
            .HasName("pk_breeds");
        
        builder.Property(b => b.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => BreedId.Create(value));
        
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(LengthConstants.LENGTH100)
            .HasColumnName("name");
    }
}