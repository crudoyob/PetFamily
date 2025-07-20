using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Extensions;
using PetFamily.Api.Extensions.VolunteerAggregate.Volunteers;
using PetFamily.Application.Volunteers.CreateVolunteer;
using PetFamily.Application.Volunteers.GetVolunteerById;
using PetFamily.Contracts.Requests;
using PetFamily.Domain.VolunteerAggregate;
using PetFamily.Domain.VolunteerAggregate.ValueObjects;

namespace PetFamily.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class VolunteersController : ApplicationController
{
    [HttpGet("{VolunteerId:guid}")]
    public async Task<ActionResult<Volunteer>> GetById(
        [FromServices] GetVolunteerByIdHandler handler,
        [FromRoute] GetVolunteerByIdRequest request, 
        CancellationToken cancellationToken)
    {
        var volunteerId = VolunteerId.Create(request.VolunteerId);

        var command = request.ToCommand();
        
        var result = await handler.Handle(command, cancellationToken);
        
        return result.ToResponse();
    }   
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        
        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }
}