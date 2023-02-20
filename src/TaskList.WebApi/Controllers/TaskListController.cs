using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;

namespace TaskList.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/taskLists")]
public class TaskListController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public TaskListController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetTaskLists(CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetTaskListsQuery(), cancellationToken));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTaskList(Guid id, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new GetTaskListQuery(id), cancellationToken));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTaskList([FromBody] CreateTaskListCommand create, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(create, cancellationToken);
        return CreatedAtAction(nameof(GetTaskList), result.Id, result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateTaskList([FromBody] UpdateTaskListCommand update, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(update, cancellationToken);
        return AcceptedAtAction(nameof(GetTaskList), result.Id, result);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTaskList(Guid id, CancellationToken cancellationToken = default)
    {
        await _mediator.Send(new DeleteTaskListCommand(id), cancellationToken);
        return NoContent();
    }
}