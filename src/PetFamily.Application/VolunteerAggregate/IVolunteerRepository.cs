using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.VolunteerAggregate;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Application.VolunteerAggregate;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken);
    
    Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken);
    Task<Result<Volunteer, Error>> GetByEmail(Email email, CancellationToken cancellationToken);
    Task<Result<Volunteer, Error>> GetByPhoneNumber(PhoneNumber phoneNumber, CancellationToken cancellationToken);
    Task<Guid> Save(Volunteer volunteer, CancellationToken cancellationToken);
}