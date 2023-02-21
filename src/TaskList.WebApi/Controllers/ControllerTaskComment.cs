using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;

namespace TaskList.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/taskComments")]
public class ControllerTaskComment : ControllerBase
{
    private readonly IMediator _mediator;

    public ControllerTaskComment(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{taskId:guid}")]
    public async Task<IActionResult> GetComments(Guid taskId, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new QueryTaskCommentGetAll(taskId), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CommandTaskCommentCreate request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(GetComments), new { taskId = request.TaskId }, result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateComment([FromBody] CommandTaskCommentUpdate request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(GetComments), new { taskId = result.TaskId }, result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteComment([FromBody] CommandTaskCommentDelete request, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(request, cancellationToken);
        return NoContent();
    }
}