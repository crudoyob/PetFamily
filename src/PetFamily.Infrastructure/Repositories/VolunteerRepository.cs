using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application;
using PetFamily.Domain.Shared.Errors;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.VolunteerAggregate;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Infrastructure.Repositories;

public class VolunteerRepository : IVolunteerRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public VolunteerRepository(ApplicationDbContext dbContext)
    {
        _dbContext =  dbContext;
    }
    
    public async Task<Result<Guid, Error>> Add(Volunteer volunteer, CancellationToken cancellationToken)
    {
        try
        {
            await _dbContext.AddAsync(volunteer, cancellationToken);
        
            await _dbContext.SaveChangesAsync(cancellationToken);
        
            return (Guid) volunteer.Id;
        }
        catch (Exception ex)
        {
            return Errors.General.Unexcepted(ex.Message);
        }
    }

    public async Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == volunteerId.Value, cancellationToken: cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(volunteerId);
        
        return volunteer;
    }
    
    public async Task<Result<Volunteer, Error>> GetByEmail(Email email, CancellationToken cancellationToken)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Email.Value == email.Value, cancellationToken);

        if (volunteer == null)
            return Errors.General.NotFound();

        return volunteer;
    }

    public async Task<Result<Volunteer, Error>> GetByPhoneNumber(
        PhoneNumber phoneNumber, CancellationToken cancellationToken)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.PhoneNumber.Value == phoneNumber.Value, cancellationToken);

        if (volunteer == null)
            return Errors.General.NotFound();

        return volunteer;
    }

    public async Task<Result<Guid, ErrorList>> Save(Volunteer volunteer, CancellationToken cancellationToken)
    {
        try
        {
            _dbContext.Attach(volunteer);
            await _dbContext.SaveChangesAsync(cancellationToken);
        
            return (Guid) volunteer.Id;
        }
        catch (Exception ex)
        {
            return Errors.General.Unexcepted(ex.Message).ToErrorList();
        }
    }
    
    public async Task<Result<Guid, Error>> Delete(Volunteer volunteer, CancellationToken cancellationToken)
    {
        try
        {
            var res = await _dbContext.Volunteers.FirstOrDefaultAsync(v => v.Id == volunteer.Id.Value,
                cancellationToken);
            
            if (res is null)
                return Errors.General.NotFound();
            
            _dbContext.Volunteers.Remove(volunteer);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return (Guid) volunteer.Id;
        }
        catch (Exception ex)
        {
            return Errors.General.Unexcepted(ex.Message);
        }
    }
}