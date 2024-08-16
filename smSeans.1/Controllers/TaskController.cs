using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;

    public TaskController(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        var task = await _taskRepository.GetTaskByIdAsync(id);
        if (task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto taskDto)
    {
        if (taskDto == null)
        {
            return BadRequest("Task data is required.");
        }

        try
        {
            var task = new Task
            {
                user_id = taskDto.user_id,
                task_description = taskDto.task_description,
                due_date = taskDto.due_date,
                status = taskDto.status,
                session_id = taskDto.session_id,
                assigned_by = taskDto.assigned_by,
                title = taskDto.title
            };

            var createdId = await _taskRepository.CreateTaskAsync(task);

            return CreatedAtAction(nameof(GetTaskById), new { id = createdId }, task);
        }
        catch (Exception ex)
        {
            // Log the exception and return an error response
            // _logger.LogError(ex, "An error occurred while creating the task.");
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskUpdateDto taskDto)
    {
        if (taskDto == null || id <= 0)
        {
            return BadRequest("Invalid task data.");
        }

        var task = await _taskRepository.GetTaskByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        task.title = taskDto.title;
        task.task_description = taskDto.task_description;
        task.due_date = taskDto.due_date;
        task.status = taskDto.status;

        await _taskRepository.UpdateTaskAsync(task);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var task = await _taskRepository.GetTaskByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        await _taskRepository.DeleteTaskAsync(id);

        return NoContent();
    }
}
