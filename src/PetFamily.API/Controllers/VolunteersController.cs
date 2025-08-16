using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Extensions;
using PetFamily.Application.VolunteerAggregate.Create;
using PetFamily.Application.VolunteerAggregate.GetById;
using PetFamily.Application.VolunteerAggregate.GetVolunteerById;
using PetFamily.Application.VolunteerAggregate.UpdateHelpRequisites;
using PetFamily.Application.VolunteerAggregate.UpdateMainInfo;
using PetFamily.Application.VolunteerAggregate.UpdateSocialNetworks;
using PetFamily.Contracts.Requests.VolunteerAggregate;
using PetFamily.Domain.VolunteerAggregate;

namespace PetFamily.Api.Controllers;

public class VolunteersController : ApplicationController
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Volunteer>> GetById(
        [FromServices] GetVolunteerByIdHandler handler,
        [FromRoute] Guid id, 
        CancellationToken cancellationToken)
    {
        var request = new GetVolunteerByIdRequest(id);
        var command = request.ToCommand();
        
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }   
    
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
       
        return Ok(result.Value);
    }
    
    [HttpPut("{id:guid}/main-info")]
    public async Task<ActionResult<Guid>> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromServices] UpdateMainInfoHandler handler,
        [FromBody] UpdateMainInfoRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
       
        return Ok(result.Value);
    }
    
    [HttpPut("{id:guid}/social-networks")]
    public async Task<ActionResult<Guid>> UpdateSocialNetworks(
        [FromRoute] Guid id,
        [FromServices] UpdateSocialNetworksHandler handler,
        [FromBody] UpdateSocialNetworksRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
       
        return Ok(result.Value);
    }
    
    [HttpPut("{id:guid}/help-requisites")]
    public async Task<ActionResult<Guid>> UpdateHelpRequisites(
        [FromRoute] Guid id,
        [FromServices] UpdateHelpRequisitesHandler handler,
        [FromBody] UpdateHelpRequisitesRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(id);
        
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
       
        return Ok(result.Value);
    }
}