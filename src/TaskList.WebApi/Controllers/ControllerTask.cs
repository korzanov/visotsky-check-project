using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;

namespace TaskList.WebApi.Controllers;

[ApiController]
[Authorize]
[Route(RouteConstants.UriTasks)]
public class ControllerTask : ControllerBase
{
    private readonly IMediator _mediator;

    public ControllerTask(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet( RouteConstants.UriTasks_List + "/{taskListId:guid}")]
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
        var task = await _mediator.Send(new QueryTaskGet(id), cancellationToken);
        var statusRecord = await _mediator.Send(new QueryTaskStatusRecordGetLast(id), cancellationToken);
        var comments = await _mediator.Send(new QueryTaskCommentGetAll(id), cancellationToken);
        return Ok(new { task = task, status = statusRecord, comments = comments });
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CommandTaskCreate request,
        CancellationToken cancellationToken = default)
    {
        var task = await _mediator.Send(request, cancellationToken);
        var defaultStatus = await _mediator.Send(new QueryTaskStatusGetDefault(), cancellationToken);
        var statusRecord = await _mediator.Send(new CommandTaskStatusRecordCreate(task.Id, defaultStatus.Id),
            cancellationToken);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, new { task = task, status = statusRecord });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] CommandTaskUpdate request,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(GetTask), new { id = result.Id }, result);
    }
    
    [HttpPut(RouteConstants.UriTasks_ListChange)]
    public async Task<IActionResult> ChangeTaskListOfTask([FromBody] CommandTaskChangeTaskList request,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(GetTask), new { id = result.Id }, result);
    }
    
    [HttpPut(RouteConstants.UriTasks_StatusChange)]
    public async Task<IActionResult> ChangeTaskStatus([FromBody] CommandTaskStatusRecordCreate request,
        CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return CreatedAtAction(nameof(GetTask), new { id = request.TaskId }, result);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteTask([FromBody] CommandTaskDelete request,
        CancellationToken cancellationToken = default)
    {
        await _mediator.Send(request, cancellationToken);
        return NoContent();
    }
}