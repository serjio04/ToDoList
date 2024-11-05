[ApiController]
[Route("api/tasks/{taskId}/subtasks")]
public class SubtasksController : ControllerBase
{
    private readonly ISubtaskService _subtaskService;

    public SubtasksController(ISubtaskService subtaskService)
    {
        _subtaskService = subtaskService;
    }

    // Получить все подзадачи для определенной задачи
    [HttpGet]
    public IActionResult GetAllSubtasks(int taskId)
    {
        var subtasks = _subtaskService.GetSubtasksByTaskId(taskId);
        return Ok(subtasks);
    }

    // Получить подзадачу по ID
    [HttpGet("{subtaskId}")]
    public IActionResult GetSubtaskById(int taskId, int subtaskId)
    {
        var subtask = _subtaskService.GetSubtaskById(subtaskId);
        if (subtask == null || subtask.TaskId != taskId)
            return NotFound();

        return Ok(subtask);
    }

    // Создать новую подзадачу
    [HttpPost]
    public IActionResult CreateSubtask(int taskId, [FromBody] Subtask subtask)
    {
        if (!ModelState.IsValid || subtask.TaskId != taskId)
            return BadRequest();

        _subtaskService.AddSubtask(subtask);
        return CreatedAtAction(nameof(GetSubtaskById), new { taskId = subtask.TaskId, subtaskId = subtask.Id }, subtask);
    }

    // Удалить подзадачу
    [HttpDelete("{subtaskId}")]
    public IActionResult DeleteSubtask(int taskId, int subtaskId)
    {
        var deleted = _subtaskService.DeleteSubtask(subtaskId);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
