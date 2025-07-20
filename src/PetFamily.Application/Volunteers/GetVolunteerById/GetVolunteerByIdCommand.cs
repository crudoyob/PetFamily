using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Application.Volunteers.GetVolunteerById;

public record GetVolunteerByIdCommand(
    VolunteerId VolunteerId);