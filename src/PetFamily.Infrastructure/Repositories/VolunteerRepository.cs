using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate;

namespace PetFamily.Infrastructure.Repositories;

public class VolunteerRepository(ApplicationDbContext dbContext) : IVolunteerRepository
{
    public async Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken)
    {
        await dbContext.AddAsync(volunteer, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return volunteer.Id;
    }

    public async Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken)
    {
        var volunteer = await dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == volunteerId.Value, cancellationToken: cancellationToken);

        if (volunteer == null)
            return Errors.General.NotFound(volunteerId);
        
        return volunteer;
    }
    
    public async Task<Result<Volunteer, Error>> GetByEmail(Email email, CancellationToken cancellationToken)
    {
        var volunteer = await dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Email.Value == email.Value, cancellationToken);

        if (volunteer == null)
            return Errors.General.NotFound();

        return volunteer;
    }

    public async Task<Result<Volunteer, Error>> GetByPhoneNumber(PhoneNumber phoneNumber, CancellationToken cancellationToken)
    {
        var volunteer = await dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.PhoneNumber.Value == phoneNumber.Value, cancellationToken);

        if (volunteer == null)
            return Errors.General.NotFound();

        return volunteer;
    }
}