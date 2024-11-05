[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    // Получить все задачи
    [HttpGet]
    public IActionResult GetAllTasks()
    {
        var tasks = _taskService.GetAllTasks();
        return Ok(tasks);
    }

    // Получить задачу по ID
    [HttpGet("{id}")]
    public IActionResult GetTaskById(int id)
    {
        var task = _taskService.GetTaskById(id);
        if (task == null)
            return NotFound();

        return Ok(task);
    }

    // Создать новую задачу
    [HttpPost]
    public IActionResult CreateTask([FromBody] Task task)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _taskService.AddTask(task);
        return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
    }

    // Обновить задачу
    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, [FromBody] Task task)
    {
        if (id != task.Id || !ModelState.IsValid)
            return BadRequest();

        var updatedTask = _taskService.UpdateTask(task);
        if (updatedTask == null)
            return NotFound();

        return Ok(updatedTask);
    }

    // Удалить задачу
    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        var deleted = _taskService.DeleteTask(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }

    // Отметить задачу как выполненную
    [HttpPatch("{id}/complete")]
    public IActionResult CompleteTask(int id)
    {
        var task = _taskService.CompleteTask(id);
        if (task == null)
            return NotFound();

        return Ok(task);
    }
}
