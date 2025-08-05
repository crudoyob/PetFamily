using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.VolunteerAggregate;

namespace PetFamily.Infrastructure.Configurations;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");
        
        builder.HasKey(v => v.Id)
            .HasName("pk_volunteers");
        
        builder.Property(v => v.Id)
            .HasColumnName("id")
            .HasConversion(
                id => id.Value,
                value => VolunteerId.Create(value));

        builder.ComplexProperty(v => v.FullName, fnb =>
        {
            fnb.Property(fn => fn.LastName)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH100)
                .HasColumnName("last_name");
            
            fnb.Property(fn => fn.FirstName)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH100)
                .HasColumnName("first_name");
            
            fnb.Property(fn => fn.Patronymic)
                .IsRequired(false)
                .HasMaxLength(LengthConstants.LENGTH100)
                .HasColumnName("patronymic");
        });

        builder.ComplexProperty(v => v.Email, eb =>
        {
            eb.Property(e => e.Value)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH100)
                .HasColumnName("email");
        });
        
        builder.Property(v => v.Description)
            .IsRequired()
            .HasMaxLength(LengthConstants.LENGTH1500)
            .HasColumnName("description");
        
        builder.Property(v => v.YearsOfExperience)
            .IsRequired()
            .HasMaxLength(LengthConstants.LENGTH100)
            .HasColumnName("years_of_experience");
        
        builder.ComplexProperty(v => v.PhoneNumber, pb =>
        {
            pb.Property(ph => ph.Value)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH100)
                .HasColumnName("phone_number");
        });

        builder.OwnsMany(v => v.SocialNetworks, snb =>
        {
            snb.ToJson("social_networks");
            
            snb.Property(sn => sn.Name)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH100)
                .HasColumnName("name");
            
            snb.Property(sn => sn.Url)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH250)
                .HasColumnName("url");
        });

        builder.OwnsMany(v => v.HelpRequisites, hrb =>
        {
            hrb.ToJson("help_requisites");
            
            hrb.Property(hr => hr.Name)
                .IsRequired()
                .HasMaxLength(LengthConstants.LENGTH100)
                .HasColumnName("name");

            hrb.Property(hr => hr.Description)
                .IsRequired(false)
                .HasMaxLength(LengthConstants.LENGTH250)
                .HasColumnName("description");
        });

        builder.HasMany(v => v.Pets)
            .WithOne(p => p.Volunteer)
            .HasForeignKey("volunteer_id")
            .HasConstraintName("fk_pets_volunteers_volunteer_id")
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}