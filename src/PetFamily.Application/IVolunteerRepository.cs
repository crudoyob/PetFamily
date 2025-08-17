using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Shared.Ids;
using PetFamily.Domain.VolunteerAggregate;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Application;

public interface IVolunteerRepository
{
    Task<Guid> Add(Volunteer volunteer, CancellationToken cancellationToken);
    
    Task<Result<Volunteer, Error>> GetById(VolunteerId volunteerId, CancellationToken cancellationToken);
    Task<Result<Volunteer, Error>> GetByEmail(Email email, CancellationToken cancellationToken);
    Task<Result<Volunteer, Error>> GetByPhoneNumber(PhoneNumber phoneNumber, CancellationToken cancellationToken);
    Task <Result<Guid, ErrorList>> Save(Volunteer volunteer, CancellationToken cancellationToken);
}