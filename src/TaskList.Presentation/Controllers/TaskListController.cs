using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskList.Contracts;
using TaskList.Services.Abstractions;

namespace TaskList.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/taskLists")]
public class TaskListController : ControllerBase
{
    private readonly ITaskListService _taskListService;

    public TaskListController(IServiceManager serviceManager)
    {
        _taskListService = serviceManager.TaskListService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTaskLists(CancellationToken cancellationToken = default)
    {
        return Ok(await _taskListService.GetAllAsync(cancellationToken));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTaskList(Guid id, CancellationToken cancellationToken = default)
    {
        return Ok(await _taskListService.GetByIdAsync(id, cancellationToken));
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTaskList([FromBody] TaskListCreateDto createDto, CancellationToken cancellationToken = default)
    {
        var result = await _taskListService.CreateAsync(createDto, cancellationToken);
        return CreatedAtAction(nameof(GetTaskList), result.Id, result);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateTaskList(Guid id, [FromBody] TaskListUpdateDto createDto, CancellationToken cancellationToken = default)
    {
        await _taskListService.UpdateAsync(id, createDto, cancellationToken);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTaskList(Guid id, CancellationToken cancellationToken = default)
    {
        await _taskListService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}