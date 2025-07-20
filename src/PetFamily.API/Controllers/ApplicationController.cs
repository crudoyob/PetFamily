using Microsoft.AspNetCore.Mvc;
using PetFamily.Contracts.Response;

namespace PetFamily.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);

        return new(envelope);
    }
}