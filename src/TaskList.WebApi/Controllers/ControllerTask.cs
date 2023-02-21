using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;

namespace TaskList.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/tasks")]
public class ControllerTask : ControllerBase
{
    private readonly IMediator _mediator;

    public ControllerTask(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("list/{taskListId:guid}")]
    public async Task<IActionResult> GetTasksByTaskList(Guid taskListId, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new QueryTaskGetAllByTaskList(taskListId), cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks(CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new QueryTaskGetAll(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTask(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new QueryTaskGet(id), cancellationToken);
        var statusRecord = await _mediator.Send(new QueryTaskStatusRecordGetLast(id), cancellationToken);
        var comments = await _mediator.Send(new QueryTaskCommentGetAll(id), cancellationToken);
        return Ok(new { task = response, status = statusRecord, comments });
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CommandTaskCreate request,
        CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(GetTask), new { id = response.Id }, response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] CommandTaskUpdate request,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(GetTask), new { id = result.Id }, result);
    }
    
    [HttpPut("changeList")]
    public async Task<IActionResult> ChangeTaskListOfTask([FromBody] CommandTaskChangeTaskList request,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(GetTask), new { id = result.Id }, result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteTask([FromBody] CommandTaskDelete request,
        CancellationToken cancellationToken = default)
    {
        await _mediator.Send(request, cancellationToken);
        return NoContent();
    }
}