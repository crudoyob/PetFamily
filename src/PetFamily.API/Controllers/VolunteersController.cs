using Microsoft.AspNetCore.Mvc;
using PetFamily.Api.Extensions;
using PetFamily.Application.VolunteerAggregate.Create;
using PetFamily.Application.VolunteerAggregate.Delete.Hard;
using PetFamily.Application.VolunteerAggregate.Delete.Soft;
using PetFamily.Application.VolunteerAggregate.Get;
using PetFamily.Application.VolunteerAggregate.Restore;
using PetFamily.Application.VolunteerAggregate.Update.HelpRequisites;
using PetFamily.Application.VolunteerAggregate.Update.MainInfo;
using PetFamily.Application.VolunteerAggregate.Update.SocialNetworks;
using PetFamily.Contracts.Requests.VolunteerAggregate;
using PetFamily.Domain.VolunteerAggregate;

namespace PetFamily.Api.Controllers;

public class VolunteersController : ApplicationController
{
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
    
    [HttpDelete("{id:guid}/hard")]
    public async Task<ActionResult<Guid>> HardDelete(
        [FromServices] HardDeleteVolunteerHandler handler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var request = new HardDeleteVolunteerRequest(id);
        var command = request.ToCommand();
        
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
       
        return Ok(result.Value);
    }
    
    [HttpDelete("{id:guid}/soft")]
    public async Task<ActionResult<Guid>> SoftDelete(
        [FromServices] SoftDeleteVolunteerHandler handler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var request = new SoftDeleteVolunteerRequest(id);
        var command = request.ToCommand();
        
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
       
        return Ok(result.Value);
    }
    
    [HttpPut("{id:guid}/restore")]
    public async Task<ActionResult<Guid>> Restore(
        [FromServices] RestoreVolunteerHandler handler,
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var request = new RestoreVolunteerRequest(id);
        var command = request.ToCommand();
        
        var result = await handler.Handle(command, cancellationToken);
        
        if (result.IsFailure)
            return result.Error.ToResponse();
       
        return Ok(result.Value);
    }
}