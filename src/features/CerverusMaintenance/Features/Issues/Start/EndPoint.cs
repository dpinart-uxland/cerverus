﻿using Cerverus.Maintenance.Features.Features.Issues.GetDetail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wolverine;

namespace Cerverus.Maintenance.Features.Features.Issues;

[ApiController]
[Route("api/maintenance-issues")]
public class MaintenanceIssuesController : ControllerBase
{
    public const string ConsumesMediaType = "application/json;domain-model=Cerverus.Maintenance.MaintenanceIssueDetail;version=1.0";
    [HttpPut("{id}/start")]
    [
        ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MaintenanceIssueDetail)),
        ProducesResponseType(StatusCodes.Status404NotFound),
        // Produces(ProducesMediaType)
    ]
    public async Task<IActionResult> Start(string id, IMessageBus bus)
    {
        await bus.SendAsync(new StartIssueResolution(id));
        return Ok("Started issue resolution.");
    }
}