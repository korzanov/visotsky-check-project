using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts.Commands;
using TaskList.Contracts.Queries;

namespace TaskList.WebApi.Controllers;

[ApiController]
[Authorize]
[Route(RouteConstants.UriTaskLists)]
public class ControllerTaskList : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ControllerTaskList(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetTaskLists(CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new QueryTaskListGetAll(), cancellationToken));
    }

    [HttpGet("page/{number:int}")]
    public async Task<IActionResult> GetTaskListsPage(int pageNumber, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new QueryTaskListGetPage(pageNumber), cancellationToken));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTaskList(Guid id, CancellationToken cancellationToken = default)
    {
        return Ok(await _mediator.Send(new QueryTaskListGet(id), cancellationToken));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTaskList([FromBody] CommandTaskListCreate commandTaskListCreate, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(commandTaskListCreate, cancellationToken);
        return CreatedAtAction(nameof(GetTaskList), new { id = result.Id }, result);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateTaskList([FromBody] CommandTaskListUpdate commandTaskListUpdate, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(commandTaskListUpdate, cancellationToken);
        return CreatedAtAction(nameof(GetTaskList), new { id = result.Id }, result);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTaskList(Guid id, CancellationToken cancellationToken = default)
    {
        var tasks = await _mediator.Send(new QueryTaskGetAllByTaskList(id), cancellationToken);
        if (tasks.Any())
            return RedirectToAction(nameof(DeleteTaskListWithReplace));
        await _mediator.Send(new CommandTaskListDelete(id), cancellationToken);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}/to/{replaceId:guid}")]
    public async Task<IActionResult> DeleteTaskListWithReplace(Guid id, Guid replaceId, CancellationToken cancellationToken = default)
    {
        var tasks = await _mediator.Send(new QueryTaskGetAllByTaskList(id), cancellationToken);
        foreach (var task in tasks)
            await _mediator.Send(new CommandTaskChangeTaskList(task.Id, replaceId), cancellationToken);
        await _mediator.Send(new CommandTaskListDelete(id), cancellationToken);
        return NoContent();
    }
}