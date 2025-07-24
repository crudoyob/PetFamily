using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.SpeciesAggregate.BreedEntity.ValueObjects;
using PetFamily.Domain.SpeciesAggregate.ValueObjects;
using PetFamily.Domain.VolunteerAggregate.PetEntity;
using PetFamily.Domain.VolunteerAggregate.PetEntity.ValueObjects;

namespace PetFamily.Infrastructure.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(p => p.Id)
            .HasName("pk_pets");

        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => PetId.Create(value));

        builder.Property(p => p.Nickname)
            .IsRequired()
            .HasMaxLength(LengthConstants.LENGTH100)
            .HasColumnName("nickname");

        builder.ComplexProperty(p => p.SpeciesBreed, sbb =>
        {
            sbb.Property(sb => sb.SpeciesId)
                    .IsRequired()
                    .HasColumnName("species_id")
                    .HasConversion(
                        id => id.Value,
                        value => SpeciesId.Create(value));

            sbb.Property(bb => bb.BreedId)
                    .IsRequired()
                    .HasColumnName("breed_id")
                    .HasConversion(
                        id => id.Value,
                        value => BreedId.Create(value));
        });

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(LengthConstants.LENGTH1500)
            .HasColumnName("description");

        builder.Property(p => p.Color)
            .IsRequired()
            .HasMaxLength(LengthConstants.LENGTH500)
            .HasColumnName("color");

        builder.Property(p => p.HealthInfo)
            .IsRequired()
            .HasMaxLength(LengthConstants.LENGTH1500)
            .HasColumnName("health_info");

        builder.ComplexProperty(p => p.Location, lb => 
        {
            lb.Property(l => l.Country)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH50)
                .HasColumnName("country");
            
            lb.Property(l => l.Region)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH100)
                .HasColumnName("region");
            
            lb.Property(l => l.City)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH100)
                .HasColumnName("city");
            
            lb.Property(l => l.District)
                .IsRequired(false)
                .HasMaxLength(LengthConstants.LENGTH100)
                .HasColumnName("district");
            
            lb.Property(l => l.Street)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH100)
                .HasColumnName("street");
            
            lb.Property(l => l.Building)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH100)
                .HasColumnName("building");
            
            lb.Property(l => l.Letter)
                .IsRequired(false)
                .HasMaxLength(LengthConstants.LENGTH1)
                .HasColumnName("letter");
            
            lb.Property(l => l.Corpus)
                .IsRequired(false)
                .HasMaxLength(LengthConstants.LENGTH100)
                .HasColumnName("corpus");
            
            lb.Property(l => l.Construction)
                .IsRequired(false)
                .HasMaxLength(LengthConstants.LENGTH100)
                .HasColumnName("construction");
            
            lb.Property(l => l.Apartment)
                .IsRequired(false)
                .HasMaxLength(LengthConstants.LENGTH100)
                .HasColumnName("apartment");
            
            lb.Property(l => l.PostalCode)
                .IsRequired(false)
                .HasMaxLength(LengthConstants.LENGTH6)
                .HasColumnName("postal_code");
        });
        
        builder.Property(p => p.Weight)
            .IsRequired()
            .HasColumnName("weight");
        
        builder.Property(p => p.Height)
            .IsRequired()
            .HasColumnName("height");

        builder.ComplexProperty(p => p.PhoneNumber, pb =>
        {
            pb.Property(ph => ph.Value)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH100)
                .HasColumnName("phone_number");
        });
        
        builder.Property(p => p.IsNeutered)
            .IsRequired()
            .HasColumnName("is_neutered");
        
        builder.Property(p => p.BirthDate)
            .IsRequired()
            .HasColumnName("birth_date");
        
        builder.Property(p => p.IsVaccinated)
            .IsRequired()
            .HasColumnName("is_vaccinated");

        builder.ComplexProperty(p => p.HelpStatus, hsb =>
        {
            hsb.Property(hs => hs.Value)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH250)
                .HasColumnName("help_status");
        });

        builder.OwnsMany(p => p.HelpRequisites, hrb =>
        {
            hrb.ToJson("help_requisites");
            
            hrb.Property(hr => hr.Name)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH100)
                .HasColumnName("name");
            
            hrb.Property(hr => hr.Description)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH250)
                .HasColumnName("description");
        });
        
        builder.Property(p => p.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at");
    }
}